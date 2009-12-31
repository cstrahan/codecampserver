using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
//using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public class UserGroupControllerTester : ControllerTester
	{
		[Test]
		public void When_a_UserGroup_does_not_exist_Edit_should_redirect_to_the_index_with_a_message()
		{
			var repository = S<IUserGroupRepository>();
			repository.Stub(repo => repo.GetById(Guid.Empty)).Return(new UserGroup());

			var controller = new UserGroupController(repository, null);

			ActionResult result = controller.Edit(Guid.Empty);
			result.AssertViewRendered().ForView("");
		}


		[Test]
		public void Should_save_the_UserGroup()
		{
			var form = new UserGroupInput();
			var UserGroup = new UserGroup();

//			var mapper = S<IUserGroupMapper>();
//			mapper.Stub(m => m.Map(form)).Return(UserGroup);

			var controller = new UserGroupController(null, PermisiveSecurityContext());
			var result = (CommandResult) controller.Edit(form);
		}
	}
}