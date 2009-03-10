using System;
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


        [Test]
        public void Should_remove_a_user_from_its_collection()
        {
            UserGroup userGroup = CreateUserGroup();

            IUserGroupRepository repository = new UserGroupRepository(new HybridSessionBuilder());
            repository.Save(userGroup);
            userGroup.Remove(userGroup.GetUsers()[0]);
            repository.Save(userGroup);

            UserGroup rehydratedConference;
            using (ISession session = GetSession())
            {
                rehydratedConference = session.Load<UserGroup>(userGroup.Id);
                rehydratedConference.GetUsers().Length.ShouldEqual(0);
            }
        }

		protected override UserGroupRepository CreateRepository()
		{
			return new UserGroupRepository(GetSessionBuilder());
		}        
	}
}