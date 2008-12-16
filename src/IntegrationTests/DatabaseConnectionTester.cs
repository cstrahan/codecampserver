using System.Data;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests
{
    [TestFixture]
    public class DatabaseConnectionTester : DataTestBase
    {
        protected override void DeleteAllObjects()
        {
            //don't do it here.
        }

        [Test]
        public void Database_connection_should_work()
        {
            ISession session = GetSession();
            IDbConnection connection = session.Connection;
            IDbCommand command = connection.CreateCommand();
            command.CommandText = "select 1+1 from Users";
            command.ExecuteNonQuery();
        }
    }
}