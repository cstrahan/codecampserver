using System.Data;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture]
	public class DatabaseConnectionTester : DataTestBase
	{
		[Test]
		public void Database_connection_should_work()
		{
			ISession session = new SessionBuilder().GetSession();
			if (session.Transaction.IsActive)
				session.Transaction.Rollback();

			IDbConnection connection = session.Connection;
			IDbCommand command = connection.CreateCommand();
			command.CommandText = "select 1+1 from Users";
			command.ExecuteNonQuery();
		}
	}
}