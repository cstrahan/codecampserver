using CodeCampServer.Core.Common;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class UserGroupRepositoryTester : KeyedRepositoryTester<UserGroup, UserGroupRepository>
	{
		private static UserGroup CreateUserGroup()
		{
			var userGroup = new UserGroup
			                	{
									Key = "theKey",
			                		Name = "sdf",
			                	};
			userGroup.Add(new User {EmailAddress = "werwer@asdfasd.com"});
			userGroup.Add(new Sponsor { Name = "sponsor 1"});
			userGroup.Add(new Sponsor { Name = "sponsor 2" });
			return userGroup;
		}

		[Test]
		public void Should_remove_a_user_from_its_collection()
		{
			UserGroup userGroup = CreateUserGroup();
			using (ISession session = GetSession())
			{
				userGroup.GetUsers().ForEach(session.SaveOrUpdate);
				session.Flush();
			}
			GetSession().Dispose();

			IUserGroupRepository repository = CreateRepository();
			repository.Save(userGroup);
			userGroup.Remove(userGroup.GetUsers()[0]);
			repository.Save(userGroup);
			CommitChanges();
			GetSession().Dispose();

			UserGroup rehydratedGroup;
			using (ISession session = GetSession())
			{
				rehydratedGroup = session.Load<UserGroup>(userGroup.Id);
				rehydratedGroup.GetUsers().Length.ShouldEqual(0);
			}
		}

		[Test]
		public void Should_add_a_sponsor_to_its_collection()
		{
			UserGroup userGroup = CreateUserGroup();
			using (ISession session = GetSession())
			{
				userGroup.GetUsers().ForEach(o => session.SaveOrUpdate(o));
				session.Flush();
			}

			IUserGroupRepository repository = CreateRepository();
			repository.Save(userGroup);
			CommitChanges();

			UserGroup rehydratedGroup;
			IUserGroupRepository repository2 = CreateRepository();
			rehydratedGroup = repository2.GetByKey(userGroup.Key);
			rehydratedGroup.GetSponsors().Length.ShouldEqual(2);

			GetSession().Flush();
		}

		[Test]
		public void Should_retrieve_the_default_usergroup()
		{
			UserGroup userGroup = CreateUserGroup();
			userGroup.Key = "localhost";

			using (ISession session = GetSession())
			{
				userGroup.GetUsers().ForEach(o => session.SaveOrUpdate(o));
				userGroup.GetSponsors().ForEach(o => session.SaveOrUpdate(o));
				session.SaveOrUpdate(userGroup);
				session.Flush();
			}


			IUserGroupRepository repository = CreateRepository();
			UserGroup group = repository.GetDefaultUserGroup();

			group.ShouldEqual(userGroup);
		}
	}
}