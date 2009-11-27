using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class HomeControllerTester : ControllerTester
	{
		[Test]
		public void About_should_go_to_about_view()
		{
			var userGroup = new UserGroup();

			var home = new HomeController();

			ViewResult result = home.About(userGroup);
			result.ViewName.ShouldEqual("");
			result.ViewData.Model.ShouldEqual(userGroup);
			((AutoMappedViewResult) result).ViewModelType.ShouldBe(typeof (UserGroupInput));
		}

		[Test]
		public void The_index_should_retrieve_the_user_group_by_its_domain_name()
		{
			var userGroup = new UserGroup();
			userGroup.Key = "adnug";

			var home = new HomeController();

			ViewResult result = home.Index(userGroup);
			result.ForView("");
			result.WithViewData<UserGroup>().ShouldNotBeNull();
			((AutoMappedViewResult) result).ViewModelType.ShouldBe(typeof (UserGroupInput));
		}
	}
}