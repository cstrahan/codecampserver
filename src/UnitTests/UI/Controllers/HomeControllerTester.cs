using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class HomeControllerTester : SaveControllerTester
	{
		[Test]
		public void About_should_go_to_about_view()
		{
			var userGroup = new UserGroup();

			var mapper = S<IUserGroupMapper>();
			var groupForm = new UserGroupInput();
			mapper.Stub(m => m.Map(userGroup)).Return(groupForm);

			var home = new HomeController(mapper);

			ViewResult result = home.About(userGroup);
			result.ViewName.ShouldEqual("");
			result.ViewData.Model.ShouldEqual(groupForm);
		}

		[Test]
		public void The_index_should_retrieve_the_user_group_by_its_domain_name()
		{
			var userGroup = new UserGroup();
		    userGroup.Key = "adnug";

			var mapper = S<IUserGroupMapper>();
			mapper.Stub(groupMapper => groupMapper.Map(userGroup)).Return(new UserGroupInput());

			var home = new HomeController(mapper);

			ViewResult result = home.Index(userGroup);
			result.ForView("");
			result.WithViewData<UserGroupInput>().ShouldNotBeNull();
		}
	}
}