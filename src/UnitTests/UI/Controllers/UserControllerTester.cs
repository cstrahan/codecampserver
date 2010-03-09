using System;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class UserControllerTester : ControllerTester
	{
		//private class TestUserMapper : UserMapper
		//{
		//    public TestUserMapper() : base(null, null) {}
		//    public User MappedUser { get; set; }

		//    public override UserInput Map(User form)
		//    {
		//        MappedUser = form;
		//        return new UserInput();
		//    }
		//}

		[Test]
		public void Edit_should_only_allow_system_admins_to_edit_other_users()
		{
			var controller = new UserController(null, RestrictiveSecurityContext());

			controller.Edit(new User())
				.AssertViewRendered()
				.ForView(ViewPages.NotAuthorized);
		}

		[Test]
		public void Index_should_list_the_users()
		{
			var controller = new UserController(S<IUserRepository>(), PermisiveSecurityContext());
			var result = controller.Index();
			result.AssertViewRendered();
			result.ForView("");
		}


		[Test]
		public void Save_should_update_an_existing_user()
		{
			var user = new User {Username = "admin", Id = Guid.NewGuid()};
			var form = new UserInput {Id = user.Id, Password = "pass"};
			var controller = new UserController(null, PermisiveSecurityContext());

			var result = (CommandResult) controller.Edit(form);

			result.Success.AssertActionRedirect().ToAction<HomeController>(a => a.Index(null));
		}

		[Test]
		public void When_edit_is_passed_null_a_new_user_should_be_selected()
		{
			var controller = new UserController(null, PermisiveSecurityContext());

			var result = controller.Edit((User) null);

			result.AssertViewRendered()
				.ForView(ViewNames.Default)
				.ModelShouldBe<User>();
			((AutoMappedViewResult) result).ViewModelType.ShouldBe(typeof (UserInput));
		}
	}
}