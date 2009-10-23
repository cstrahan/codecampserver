using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.UI.Mappers;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using CommandProcessor;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.RulesEngine;

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
			var controller = new UserController(null, null, RestrictiveSecurityContext(), S<IUserSession>(), null);

			controller.Edit(new User())
				.AssertViewRendered()
				.ForView(ViewPages.NotAuthorized);
		}

		[Test]
		public void Index_should_list_the_users()
		{
			var controller = new UserController(S<IUserRepository>(), S<IUserMapper>(), PermisiveSecurityContext(), null, null);
			ViewResult result = controller.Index();
			result.AssertViewRendered();
			result.ForView("");
		}



		[Test]
		public void Save_should_update_an_existing_user()
		{
			var user = new User {Username = "admin", Id = Guid.NewGuid()};
			var form = new UserInput {Id = user.Id, Password = "pass"};
			var engine = S<IRulesEngine>();
			engine.Stub(rulesEngine => rulesEngine.Process(form)).Return(new ExecutionResult());
			var controller = new UserController(null, null, PermisiveSecurityContext(), null, engine);

			var result = (RedirectToRouteResult) controller.Edit(form);

			result.AssertActionRedirect().ToAction<HomeController>(a => a.Index(null));
		}

		[Test]
		public void When_edit_is_passed_null_a_new_user_should_be_selected()
		{
			var mapper = new TestUserMapper();

			var controller = new UserController(null, mapper, PermisiveSecurityContext(), null, null);

			controller.Edit((User)null)
				.AssertViewRendered()
				.ForView(ViewNames.Default)
				.ModelShouldBe<UserInput>();
			mapper.MappedUser.ShouldNotBeNull();
			mapper.MappedUser.Id.ShouldEqual(Guid.Empty);
		}

	}
}