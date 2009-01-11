using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class AdminControllerTester : SaveControllerTester
	{
		[Test]
		public void When_new_user_is_saved_Should_map_from_form_and_call_repository()
		{
			var form = new UserForm
			           	{
			           		Id = Guid.Empty,
			           		Username = "username",
			           		Password = "password",
			           		EmailAddress = "email",
			           		Name = "name"
			           	};

			var newUser = new User();
			var mapper = S<IUserMapper>();
			mapper.Stub(m => m.Map(form)).Return(newUser);

			var controller = new AdminController(S<IUserRepository>(), mapper);
			var result = controller.Save(form);
			result.AssertActionRedirect();
		}

		[Test]
		public void When_a_user_does_not_exist_Contoller_should_redirect_to_edit_screen_when_there_are_zero_users()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(null);

			var controller = new AdminController(repository, S<IUserMapper>());
			var result = controller.Index();
			result.AssertActionRedirect();

			var redirectResult = result as RedirectToRouteResult;
			redirectResult.ToAction<AdminController>(a => a.Edit(null));
		}

		[Test]
		public void When_a_user_does_not_exist_Edit_should_render_the_edit_view()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(null);
			repository.Stub(repo => repo.GetAll()).Return(new User[0]);

			var mapper = new TestUserMapper();
			var controller = new AdminController(repository, mapper);
			var result = controller.Edit(null);
			mapper.MappedUser.ShouldNotBeNull();
			mapper.MappedUser.Username.ShouldEqual("admin");
			result.AssertViewRendered().ForView(ViewNames.Default);
			result.ViewData.Model.ShouldBeInstanceOfType(typeof (UserForm));
		}

		private class TestUserMapper : UserMapper
		{
			public User MappedUser { get; set; }

			public TestUserMapper() : base(null, null)
			{
			}

			public override UserForm Map(User form)
			{
				MappedUser = form;
				return new UserForm();
			}
		}

		[Test]
		public void When_a_user_exists_Save_should_a_valid_user()
		{
			var user = new User {Username = "admin", Id = Guid.NewGuid()};
			var mapper = S<IUserMapper>();
			var form = new UserForm {Id = user.Id, Password = "pass"};
			mapper.Stub(u => u.Map(form)).Return(user);
			var repository = S<IUserRepository>();
			var controller = new AdminController(repository, mapper);

			var result = (RedirectToRouteResult) controller.Save(form);

			repository.AssertWasCalled(r => r.Save(user));
			result.AssertActionRedirect().ToAction<AdminController>(a => a.Index());
		}

		[Test]
		public void Should_not_save_user_if_key_already_exists()
		{
			var form = new UserForm {Username = "foo"};
			var user = new User();

			var mapper = S<IUserMapper>();
			mapper.Stub(m => m.Map(form)).Return(user);

			var repository = S<IUserRepository>();
			repository.Stub(r => r.GetByKey("foo")).Return(new User());

			var controller = new AdminController(repository, mapper);
			var result = (ViewResult) controller.Save(form);

			result.AssertViewRendered().ViewName.ShouldEqual("Edit");
			controller.ModelState.Values.Count.ShouldEqual(1);
			controller.ModelState["Username"].Errors[0].ErrorMessage.ShouldEqual("This username already exists");
		}
	}
}