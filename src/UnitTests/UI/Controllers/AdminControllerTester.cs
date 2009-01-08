using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class AdminControllerTester : TestControllerBase
	{
		[Test]
		public void
			When_an_admin_object_does_not_exist_Contoller_should_redirect_to_admin_password_form_when_there_are_zero_users
			()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(null);

			var controller = new AdminController(repository);
			var result = controller.Index();
			result.AssertActionRedirect();

			var redirectResult = result as RedirectToRouteResult;
			redirectResult.ToAction<AdminController>(a => a.EditAdminPassword());
		}

		[Test]
		public void
			When_an_admin_object_does_not_exist_Edit_admin_password_should_render_the_editAdmin_view
			()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(null);

			var controller = new AdminController(repository);
			var result = controller.EditAdminPassword();
			result
				.AssertViewRendered()
				.ForView(DEFAULT_VIEW)
				.WithViewData<UserForm>()
				.ShouldNotBeNull();

			repository.AssertWasCalled(r => r.Save(null), o => o.IgnoreArguments());
		}

		[Test]
		public void When_an_admin_object_exists_Save_should_a_valid_user()
		{
			var user = new User() {Username = "admin", Id = Guid.NewGuid()};
			var repository = S<IUserRepository>();
			var controller = new AdminController(repository);

			repository.Stub(repo => repo.GetByUserName("admin")).Return(user);

			var form = new UserForm() {Id = user.Id, Password = "pass"};

			repository.Stub(c => c.GetById(user.Id)).Return(user);

			ActionResult result = controller.Save(form);

			result
				.AssertActionRedirect()
				.ToAction<AdminController>(a => a.Index());

			user.PasswordHash.ShouldEqual("pass");

			repository.AssertWasCalled(r => r.Save(user));
		}
	}
}