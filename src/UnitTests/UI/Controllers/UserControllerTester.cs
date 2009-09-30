using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class UserControllerTester : SaveControllerTester
	{
		private class TestUserMapper : UserMapper
		{
			public TestUserMapper() : base(null, null) {}
			public User MappedUser { get; set; }

			public override UserInput Map(User form)
			{
				MappedUser = form;
				return new UserInput();
			}
		}

		[Test]
		public void Edit_should_only_allow_system_admins_to_edit_other_users()
		{
			var controller = new UserController(null, null, RestrictiveSecurityContext(), S<IUserSession>());

			controller.Edit(new User())
				.AssertViewRendered()
				.ForView(ViewPages.NotAuthorized);
		}

		[Test]
		public void Index_should_list_the_users()
		{
			var controller = new UserController(S<IUserRepository>(), S<IUserMapper>(), PermisiveSecurityContext(), null);
			ViewResult result = controller.Index();
		}

		[Test]
		public void New_should_render_the_edit_view()
		{
			//Arrange
			new UserController(null, null, PermisiveSecurityContext(), S<IUserSession>())
			
				//Act
				.New()
			
				//Assert
				.ForView("Edit")
				.ModelShouldBe<UserInput>();
		}


		[Test]
		public void Save_should_update_an_existing_user()
		{
			var user = new User {Username = "admin", Id = Guid.NewGuid()};
			var mapper = S<IUserMapper>();
			var form = new UserInput {Id = user.Id, Password = "pass"};
			mapper.Stub(u => u.Map(form)).Return(user);
			var repository = S<IUserRepository>();
			var controller = new UserController(repository, mapper, PermisiveSecurityContext(), S<IUserSession>());

			var result = (RedirectToRouteResult) controller.Edit(form);

			repository.AssertWasCalled(r => r.Save(user));
			result.AssertActionRedirect().ToAction<HomeController>(a => a.Index(null));
		}

		[Test]
		public void Should_not_save_user_if_key_already_exists()
		{
			var form = new UserInput {Username = "foo"};
			var user = new User();

			var mapper = S<IUserMapper>();
			mapper.Stub(m => m.Map(form)).Return(user);

			var repository = S<IUserRepository>();
			repository.Stub(r => r.GetByKey("foo")).Return(new User());

			var controller = new UserController(repository, mapper, PermisiveSecurityContext(), S<IUserSession>());
			var result = (ViewResult) controller.Edit(form);

			result.AssertViewRendered().ViewName.ShouldEqual("Edit");
			controller.ModelState.Values.Count.ShouldEqual(1);
			controller.ModelState["Username"].Errors[0].ErrorMessage.ShouldEqual("This username already exists");
		}

		[Test]
		public void When_edit_is_passed_null_the_current_user_should_be_selected()
		{
			var mapper = new TestUserMapper();
			var session = S<IUserSession>();

			var user = new User {Id = Guid.NewGuid()};
			session.Stub(userSession => userSession.GetCurrentUser()).Return(user);
			var controller = new UserController(null, mapper, PermisiveSecurityContext(), session);

			controller.Edit((User)null)
				.AssertViewRendered()
				.ForView(ViewNames.Default)
				.ModelShouldBe<UserInput>();
			mapper.MappedUser.ShouldNotBeNull();
			mapper.MappedUser.Id.ShouldEqual(user.Id);
		}

		[Test]
		public void When_new_user_is_saved_Should_map_from_form_and_call_repository()
		{
			var form = new UserInput
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

			var controller = new UserController(S<IUserRepository>(), mapper, PermisiveSecurityContext(), S<IUserSession>());
			ActionResult result = controller.Edit(form);
			result.AssertActionRedirect();
		}
	}
}