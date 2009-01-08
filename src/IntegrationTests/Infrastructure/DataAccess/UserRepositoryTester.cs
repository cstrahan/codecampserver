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
	public class UserRepositoryTester : RepositoryTester<User, UserRepository>
	{
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

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(one);
				session.SaveOrUpdate(two);
				session.SaveOrUpdate(three);
				session.Flush();
			}

			var repository = (UserRepository) ObjectFactory.GetInstance<IUserRepository>();
			User employee = repository.GetByUserName("bsimpson");

			Assert.That(employee.Id, Is.EqualTo(two.Id));
			Assert.That(employee.Username, Is.EqualTo(two.Username));
		}

		[Test]
		public void Should_get_by_last_name_start_text()
		{
			var user = new User {Name = "test1"};
			var user1 = new User {Name = "test2"};
			var user2 = new User();
			PersistEntities(user, user1, user2);
			IUserRepository repository = ObjectFactory.GetInstance<UserRepository>();

			User[] users = repository.GetLikeLastNameStart("test");

			users.Length.ShouldEqual(2);
			users[0].ShouldEqual(user);
			users[1].ShouldEqual(user1);
		}
	}
}