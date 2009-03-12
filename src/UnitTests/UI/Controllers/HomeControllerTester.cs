using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib.TestHelper;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class HomeControllerTester : SaveControllerTester
	{
		[Test]
		public void The_index_should_retrieve_the_user_group_by_its_domain_name()
		{
            UserGroup  userGroup =  new UserGroup();


		    var mapper = S<IUserGroupMapper>();
		    mapper.Stub(groupMapper => groupMapper.Map(userGroup)).Return(new UserGroupForm());

		    var home = new HomeController(mapper);

            ViewResult result = home.Index(userGroup,S<IConferenceRepository>());
		    result.ForView("");
            result.WithViewData<UserGroupForm>().ShouldNotBeNull();
		}

	}
}