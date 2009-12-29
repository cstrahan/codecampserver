using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class ChangeAuditInfoInterceptorTester : DataTestBase
	{
		private static User CurrentUser;

		private static void ResetCurrentUser()
		{
			var userSession = MockRepository.GenerateStub<IUserSession>();
			CurrentUser = new User();
			userSession.Stub(us => us.GetCurrentUser()).Return(CurrentUser);
			ObjectFactory.Inject(userSession);
		}

		[Test]
		public void should_create_audit_info_on_save_and_update_child_entities()
		{
			ResetCurrentUser();
			User user1 = CurrentUser;
			user1.Username = "foo";
			PersistEntities(user1);

			var group = new UserGroup();
			var sponsor = new Sponsor();
			group.Add(sponsor);
			PersistEntitiesWithAuditing(user1, new DateTime(2009, 4, 5), group, sponsor);
			var group1 = GetAuditedSession(user1, new DateTime(2009, 4, 5)).Load<UserGroup>(group.Id);
			group1.ChangeAuditInfo.Created.ShouldEqual(new DateTime(2009, 4, 5));
			group1.ChangeAuditInfo.CreatedBy.ShouldEqual(user1.Username);
			group1.ChangeAuditInfo.Updated.ShouldEqual(new DateTime(2009, 4, 5));
			group1.ChangeAuditInfo.UpdatedBy.ShouldEqual(user1.Username);
			Sponsor sponsor1 = group.GetSponsors()[0];
			sponsor1.ChangeAuditInfo.Created.ShouldEqual(new DateTime(2009, 4, 5));
			sponsor1.ChangeAuditInfo.CreatedBy.ShouldEqual(user1.Username);
			sponsor1.ChangeAuditInfo.Updated.ShouldEqual(new DateTime(2009, 4, 5));
			sponsor1.ChangeAuditInfo.UpdatedBy.ShouldEqual(user1.Username);
		}

		[Test]
		public void should_not_update_user_audit_info()
		{
			ResetCurrentUser();
			var user = new User();

			PersistEntities(CurrentUser);

			PersistEntitiesWithAuditing(CurrentUser, new DateTime(2009, 1, 1), user);

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
				new SessionFactoryBuilder().GetFactory().OpenSession(new ChangeAuditInfoInterceptor(new UserSessionStub(user),
				                                                                          new Clock(today)));
		}
	}
}