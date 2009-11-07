using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.DataAccess;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class ChangeAuditInfoInterceptorTester : DataTestBase
	{
		[Test]
		public void should_create_audit_info_on_save_and_update_child_entities()
		{
			TestHelper.ResetCurrentUser();
			User user1 = TestHelper.CurrentUser;
			PersistEntities(user1);

			var group = new UserGroup();
			var sponsor = new Sponsor();
			group.Add(sponsor);
			PersistEntitiesWithAuditing(user1, new DateTime(2009, 4, 5), group, sponsor);
			var group1 = GetAuditedSession(user1, new DateTime(2009, 4, 5)).Load<UserGroup>(group.Id);
			group1.ChangeAuditInfo.Created.ShouldEqual(new DateTime(2009, 4, 5));
			group1.ChangeAuditInfo.CreatedBy.ShouldEqual(user1);
			group1.ChangeAuditInfo.Updated.ShouldEqual(new DateTime(2009, 4, 5));
			group1.ChangeAuditInfo.UpdatedBy.ShouldEqual(user1);
			Sponsor sponsor1 = group.GetSponsors()[0];
			sponsor1.ChangeAuditInfo.Created.ShouldEqual(new DateTime(2009, 4, 5));
			sponsor1.ChangeAuditInfo.CreatedBy.ShouldEqual(user1);
			sponsor1.ChangeAuditInfo.Updated.ShouldEqual(new DateTime(2009, 4, 5));
			sponsor1.ChangeAuditInfo.UpdatedBy.ShouldEqual(user1);
		}

		[Test]
		public void should_not_update_user_audit_info()
		{
			TestHelper.ResetCurrentUser();
			var user = new User();

			PersistEntities(TestHelper.CurrentUser);

			PersistEntitiesWithAuditing(TestHelper.CurrentUser, new DateTime(2009, 1, 1), user);

			user.ChangeAuditInfo.Created.ShouldBeNull();
			user.ChangeAuditInfo.CreatedBy.ShouldBeNull();
			user.ChangeAuditInfo.Updated.ShouldBeNull();
			user.ChangeAuditInfo.UpdatedBy.ShouldBeNull();

			using (ISession session = GetSession())
			{
				var persistedUser = session.Load<User>(user.Id);
				session.SaveOrUpdate(persistedUser);
				session.Flush();
				persistedUser.ChangeAuditInfo.Created.ShouldBeNull();
				persistedUser.ChangeAuditInfo.CreatedBy.ShouldBeNull();
				persistedUser.ChangeAuditInfo.Updated.ShouldBeNull();
				persistedUser.ChangeAuditInfo.UpdatedBy.ShouldBeNull();
			}
		}

		protected void PersistEntitiesWithAuditing(User user, DateTime today, params AuditedPersistentObject[] entities)
		{
			using (ISession session = GetAuditedSession(user, today))
			{
				Persist(entities, session);
			}
		}

		protected virtual ISession GetAuditedSession(User user, DateTime today)
		{
			return
				TestHelper.GetSessionFactory().OpenSession(new ChangeAuditInfoInterceptor(new UserSessionStub(user),
				                                                                          new Clock(today)));
		}
	}
}