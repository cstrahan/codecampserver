using System.Data;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class UserMappingsTester : DataTestBase
	{
		[Test]
		public void Should_map_user()
		{
			var user = new User
			           	{
			           		EmailAddress = "jdoe@abc.com",
			           		Name = "sdf",
			           		Username = "jdoe",
										PasswordHash = "foo",
										PasswordSalt = "bar"
			           	};

			AssertObjectCanBePersisted(user);
		}

		[Test]
		public void User_should_be_cache_enabled()
		{
			var user = new User {Username = "foo"};

			ISession session = GetSession();
			ITransaction transaction = session.BeginTransaction();
			session.SaveOrUpdate(user);
			transaction.Commit();

			session.Dispose();

			ISession session2 = GetSession();
			var result =
				session2.CreateQuery("from User u where u.Username = ?").SetString(0,
				                                                                   "foo").
					SetCacheable(true).UniqueResult<User>();
			IDbCommand command = session2.Connection.CreateCommand();
			command.CommandText = "delete from Users";
			command.ExecuteNonQuery();
			session2.Dispose();

			var result2 =
				GetSession().CreateQuery("from User u where u.Username = ?").SetString(0,
				                                                                       "foo")
					.SetCacheable(true).UniqueResult
					<User>();
			Assert.That(result2, Is.EqualTo(result));
			Assert.That(result2, Is.Not.SameAs(result));
		}
	}
}