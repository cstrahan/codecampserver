using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
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
	public class UserGroupControllerTester : SaveControllerTester
	{
		[Test]
		public void When_a_UserGroup_does_not_exist_Edit_should_redirect_to_the_index_with_a_message()
		{
			var repository = S<IUserGroupRepository>();
			repository.Stub(repo => repo.GetById(Guid.Empty)).Return(new UserGroup());

			var controller = new UserGroupController(repository, S<IUserGroupMapper>(),null);

			ActionResult result = controller.Edit(Guid.Empty);
			result.AssertViewRendered().ForView("");
		}


		[Test]
		public void Should_save_the_UserGroup()
		{
			var form = new UserGroupInput();
			var UserGroup = new UserGroup();

			var mapper = S<IUserGroupMapper>();
			mapper.Stub(m => m.Map(form)).Return(UserGroup);

			var controller = new UserGroupController(null, mapper, PermisiveSecurityContext());
			var result = (CommandResult) controller.Edit(form);

			result.Success.AssertActionRedirect().ToAction<HomeController>(a => a.Index(null));
		}

	}
}