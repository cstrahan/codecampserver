using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class AdminControllerTester : ControllerTester
	{
		[Test]
		public void When_a_user_does_exist_Contoller_should_show_the_default_view()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(new User());

			var controller = new AdminController(repository);
			ActionResult result = controller.Index(null);
			result.AssertViewRendered().ViewName.ShouldEqual(ViewNames.Default);
		}

		[Test]
		public void When_a_user_does_not_exist_Contoller_should_redirect_to_edit_screen_when_there_are_zero_users()
		{
			var repository = S<IUserRepository>();
			repository.Stub(repo => repo.GetByUserName("admin")).Return(null);
			var controller = new AdminController(repository);
			ActionResult result = controller.Index(null);
			result.AssertActionRedirect();

			var redirectResult = result as RedirectToRouteResult;
			redirectResult.ToAction<UserController>(a => a.Edit((UserInput) null));
		}
	}
}