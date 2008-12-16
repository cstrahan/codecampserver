using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class UserRepositoryTester : DataTestBase
	{
		[Test]
		public void should_find_all_users()
		{
			var user1 = new User {};
			var user2 = new User {};

			using (var session = GetSession())
			{
				session.SaveOrUpdate(user1);
				session.SaveOrUpdate(user2);
				session.Flush();
			}

			var repos = ObjectFactory.GetInstance<IUserRepository>();

			var persistedUsers = repos.GetAll();
			CollectionAssert.AreEquivalent(new[] {user1, user2}, persistedUsers);
		}

		[Test]
		public void Should_find_employee_by_username()
		{

			var one = new User
			          	{
			          		Username = "hsimpson",
			          	};

			var two = new User
			          	{
			          		Username = "bsimpson",
			          	};

			var three = new User
			            	{
			            		Username = "lsimpson",
			            	};

			using (var session = GetSession())
			{
				session.SaveOrUpdate(one);
				session.SaveOrUpdate(two);
				session.SaveOrUpdate(three);
				session.Flush();
			}

			var repository = (UserRepository)ObjectFactory.GetInstance<IUserRepository>();
			User employee = repository.GetByUserName("bsimpson");

			Assert.That(employee.Id, Is.EqualTo(two.Id));
			Assert.That(employee.Username, Is.EqualTo(two.Username));
		}

		[Test]
		public void Should_save_user()
		{
			var user = new User
			           	{
			           		Username = "username",
			           		EmailAddress = "user@example.com",
                            HashedPassword = "hash",
                            Name = "admin"
			           	};


			var repository = ObjectFactory.GetInstance<IUserRepository>();
			repository.Save(user);

			using (ISession session = GetSession())
			{
				var rehydratedUser = session.Load<User>(user.Id);

				Assert.That(rehydratedUser.Id, Is.EqualTo(user.Id));
				Assert.That(rehydratedUser.EmailAddress, Is.EqualTo(user.EmailAddress));
				Assert.That(rehydratedUser.Username, Is.EqualTo(user.Username));
                Assert.That(rehydratedUser.Name, Is.EqualTo(user.Name));
                Assert.That(rehydratedUser.HashedPassword, Is.EqualTo(user.HashedPassword));
            }
		}

		[Test]
		public void Should_get_by_id()
		{
			var user = new User();

			using (var session = GetSession())
			{
				session.Save(user);
				session.Flush();
			}

			IUserRepository repository = ObjectFactory.GetInstance<UserRepository>();
			var user1 = repository.GetById(user.Id);
			Assert.That(user1, Is.EqualTo(user));
		}

		[Test]
		public void Should_get_by_last_name_start_text()
		{
            var user = new User() { Name = "test1" };
            var user1 = new User() { Name = "test2" };
			var user2 = new User ();
			PersistEntities(user, user1, user2);
			IUserRepository repository = ObjectFactory.GetInstance<UserRepository>();

			User[] users = repository.GetLikeLastNameStart("test");

			users.Length.ShouldEqual(2);
			users[0].ShouldEqual(user);
			users[1].ShouldEqual(user1);

		}
	}
}