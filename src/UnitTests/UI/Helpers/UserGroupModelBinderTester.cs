using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Binders;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Helpers
{
	[TestFixture]
	public class UserGroupModelBinderTester : BinderTester
	{
		[Test]
		public void The_binder_should_find_a_usergroup_by_its_key()
		{
			var userGroupRepository = S<IUserGroupRepository>();
			userGroupRepository.Stub(repository => repository.GetByKey("")).Return(null);
			var userGroup1 = new UserGroup();
			userGroupRepository.Stub(repository => repository.GetDefaultUserGroup()).Return(userGroup1);

			var modelBinder = new UserGroupModelBinder(userGroupRepository);
			var usergroup = (UserGroup) modelBinder.BindModel(new ControllerContext(), CreateBindingContext("usergroupkey", ""));

			usergroup.ShouldEqual(userGroup1);
		}

		[Test]
		public void The_binder_should_find_a_usergroup_by_its_key1()
		{
			var userGroupRepository = S<IUserGroupRepository>();
			userGroupRepository.Stub(repository => repository.GetByKey("foo")).Return(null);
			var userGroup1 = new UserGroup();
			userGroupRepository.Stub(repository => repository.GetDefaultUserGroup()).Return(userGroup1);

			var modelBinder = new UserGroupModelBinder(userGroupRepository);
			var usergroup = (UserGroup) modelBinder.BindModel(new ControllerContext(), CreateBindingContext("usergroupkey", "foo"));

			usergroup.ShouldEqual(userGroup1);
		}

		[Test]
		public void The_binder_should_find_a_usergroup_by_its_key2()
		{
			var userGroupRepository = S<IUserGroupRepository>();
			var userGroup1 = new UserGroup();
			userGroupRepository.Stub(repository => repository.GetByKey("foo")).Return(userGroup1);


			var modelBinder = new UserGroupModelBinder(userGroupRepository);
			var usergroup = (UserGroup) modelBinder.BindModel(new ControllerContext(), CreateBindingContext("usergroupkey", "foo"));
			usergroup.ShouldEqual(userGroup1);
		}
	}
}