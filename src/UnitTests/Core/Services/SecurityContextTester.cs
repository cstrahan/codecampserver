using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services
{
	[TestFixture]
	public class SecurityContextTester : TestBase
	{
		[Test]
		public void The_security_context_should_find_a_user_has_permissions()
		{
			var session = S<IUserSession>();
			var user = new User();
			session.Stub(userSession => userSession.GetCurrentUser()).Return(user);

			var usergroup = new UserGroup();
			usergroup.Add(user);

			ISecurityContext context = new SecurityContext(session, null);


			bool hasPermission = context.HasPermissionsFor(usergroup);
			hasPermission.ShouldBeTrue();
		}

		[Test]
		public void The_security_context_should_find_a_user_does_not_have_permissions()
		{
			var session = S<IUserSession>();
			var user = new User();
			session.Stub(userSession => userSession.GetCurrentUser()).Return(user);

			var usergroup = new UserGroup();

			var userGroupRepo = S<IUserGroupRepository>();
			userGroupRepo.Stub(repository => repository.GetDefaultUserGroup()).Return(new UserGroup());

			ISecurityContext context = new SecurityContext(session, userGroupRepo);


			bool hasPermission = context.HasPermissionsFor(usergroup);
			hasPermission.ShouldBeFalse();
		}

		[Test]
		public void The_security_context_should_allow_admins_to_create_new_users_groups()
		{
			var session = S<IUserSession>();
			var user = new User();
			session.Stub(userSession => userSession.GetCurrentUser()).Return(user);


			var userGroupRepo = S<IUserGroupRepository>();
			var userGroup = new UserGroup();
			userGroup.Add(user);

			userGroupRepo.Stub(repository => repository.GetDefaultUserGroup()).Return(userGroup);

			ISecurityContext context = new SecurityContext(session, userGroupRepo);


			bool hasPermission = context.HasPermissionsFor(null);

			hasPermission.ShouldBeTrue();
		}


		[Test]
		public void The_security_context_should_allow_a_system_admin_to_access_a_group()
		{
			var session = S<IUserSession>();
			var user = new User();
			session.Stub(userSession => userSession.GetCurrentUser()).Return(user);

			var userGroupRepo = S<IUserGroupRepository>();
			var defaultUserGroup = new UserGroup();
			defaultUserGroup.Add(user);
			userGroupRepo.Stub(repository => repository.GetDefaultUserGroup()).Return(defaultUserGroup);

			ISecurityContext context = new SecurityContext(session, userGroupRepo);

			bool hasPermission = context.IsAdmin();
			hasPermission.ShouldBeTrue();
		}
	}
}