using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;
using Rhino.Mocks.Interfaces;

namespace CodeCampServer.UnitTests.UI.Controllers
{

    public class When_an_admin_object_does_not_exist:TestControllerBase<AdminController>
    {
        private IUserRepository userRepository;

        protected override AdminController CreateController()
        {
            userRepository = Mock<IUserRepository>();
            userRepository.Stub(repo => repo.GetByUserName("admin")).Return(null);

            return new AdminController(userRepository);
        }

        [Test]
        public void Contoller_should_redirect_to_admin_password_form_when_there_are_zero_users()
        {
            var result = controllerUnderTest.Index();            
            result.AssertActionRedirect();

            var redirectResult = result as RedirectToRouteResult;
            redirectResult.ToAction<AdminController>(a => a.EditAdminPassword());            
        }

        [Test]
        public void Edit_admin_password_should_render_the_editAdmin_view()
        {
            var result = controllerUnderTest.EditAdminPassword();
                result
                    .AssertViewRendered()
                    .ForView(DEFAULT_VIEW)
                    .WithViewData<UserForm>()
                    .ShouldNotBeNull();
            
            userRepository.AssertWasCalled(r=>r.Save(null),o=>o.IgnoreArguments());
        }
    }
    
    public class When_an_admin_object_exists : TestControllerBase<AdminController>
    {
        private IUserRepository userRepository;
        private User user;

        protected override AdminController CreateController()
        {
            user = new User(){Username = "admin",Id = Guid.NewGuid()};
            userRepository = Mock<IUserRepository>();
            userRepository.Stub(repo => repo.GetByUserName("admin")).Return(user);

            return new AdminController(userRepository);
        }

        [Test]
        public void Save_should_a_valid_user()
        {
            var form = new UserForm() { Id = user.Id, Password = "pass" };

            userRepository.Stub(c => c.GetById(user.Id)).Return(user);
            
            ActionResult result = controllerUnderTest.Save(form);

            result
                .AssertActionRedirect()
                .ToAction<AdminController>(a => a.Index());

            user.HashedPassword.ShouldEqual("pass");

            userRepository.AssertWasCalled(r => r.Save(user));
        }
    }
}