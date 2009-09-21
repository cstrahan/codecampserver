using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class UserGroupRepositoryTester : KeyedRepositoryTester<UserGroup, UserGroupRepository>
	{
		private static UserGroup CreateUserGroup()
		{
			var userGroup = new UserGroup
			                	{
			                		Name = "sdf",
			                	};
			userGroup.Add(new User {EmailAddress = "werwer@asdfasd.com"});
			return userGroup;
		}

		protected override UserGroupRepository CreateRepository()
		{
			return new UserGroupRepository(GetSessionBuilder());
		}


		[Test]
		public void Should_remove_a_user_from_its_collection()
		{
			UserGroup userGroup = CreateUserGroup();
			using (ISession session = GetSession())
			{
				userGroup.GetUsers().ForEach(o => session.SaveOrUpdate(o));
				session.Flush();
			}

			IUserGroupRepository repository = new UserGroupRepository(new HybridSessionBuilder());
			repository.Save(userGroup);
			userGroup.Remove(userGroup.GetUsers()[0]);
			repository.Save(userGroup);

			UserGroup rehydratedGroup;
			using (ISession session = GetSession())
			{
				rehydratedGroup = session.Load<UserGroup>(userGroup.Id);
				rehydratedGroup.GetUsers().Length.ShouldEqual(0);
			}
		}

		[Test]
		public void Should_retrieve_the_default_usergroup()
		{
			UserGroup userGroup = CreateUserGroup();
			userGroup.Key = "localhost";

			using (ISession session = GetSession())
			{
				userGroup.GetUsers().ForEach(o => session.SaveOrUpdate(o));
				session.SaveOrUpdate(userGroup);
				session.Flush();
			}


			IUserGroupRepository repository = new UserGroupRepository(new HybridSessionBuilder());
			UserGroup group = repository.GetDefaultUserGroup();

			group.ShouldEqual(userGroup);
		}
	}
}