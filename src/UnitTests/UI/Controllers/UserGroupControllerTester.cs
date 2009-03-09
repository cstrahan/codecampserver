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
	public class UserGroupControllerTester : SaveControllerTester
	{
		[Test]
		public void When_a_UserGroup_does_not_exist_Edit_should_redirect_to_the_index_with_a_message()
		{
			var repository = S<IUserGroupRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new UserGroup[0]);

			var controller = new UserGroupController(repository, null);

			ActionResult result = controller.Edit(null);
			result.AssertActionRedirect().ToAction<UserGroupController>(e => e.List());
			controller.TempData["Message"].ShouldEqual(
				"UserGroup has been deleted.");
		}

		[Test]
		public void
			When_a_UserGroup_does_not_exist_Index_action_should_redirect_to_new_when_UserGroup_does_not_exist
			()
		{
			var repository = S<IUserGroupRepository>();
			repository.Stub(repo => repo.GetAll()).Return(new UserGroup[0]);

			var controller = new UserGroupController(repository, null);

			ActionResult result = controller.List();

			result.AssertActionRedirect().ToAction<UserGroupController>(a => a.New());
		}

		[Test]
		public void Should_save_the_UserGroup()
		{
			var form = new UserGroupForm();
			var UserGroup = new UserGroup();

			var mapper = S<IUserGroupMapper>();
			mapper.Stub(m => m.Map(form)).Return(UserGroup);

			var repository = S<IUserGroupRepository>();

			var controller = new UserGroupController(repository, mapper);
			var result = (RedirectToRouteResult) controller.Save(form);

			repository.AssertWasCalled(r => r.Save(UserGroup));
			result.AssertActionRedirect().ToAction<UserGroupController>(a => a.List());
		}

		[Test]
		public void Should_not_save_UserGroup_if_key_already_exists()
		{
			var form = new UserGroupForm {Key = "foo", Id = Guid.NewGuid()};
			var UserGroup = new UserGroup();

			var mapper = S<IUserGroupMapper>();
			mapper.Stub(m => m.Map(form)).Return(UserGroup);

			var repository = S<IUserGroupRepository>();
			repository.Stub(r => r.GetByKey("foo")).Return(new UserGroup());

			var controller = new UserGroupController(repository, mapper);
			var result = (ViewResult) controller.Save(form);

			result.AssertViewRendered().ViewName.ShouldEqual("Edit");
			controller.ModelState.Values.Count.ShouldEqual(1);
			controller.ModelState["Key"].Errors[0].ErrorMessage.ShouldEqual("This entity key already exists");
		}
	}
}