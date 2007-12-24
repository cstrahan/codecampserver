using System.Data;
using CodeCampServer.DataAccess;
using CodeCampServer.DataAccess.Impl;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    public class DatabaseTesterBase : RepositoryBase
    {
        protected static ISessionBuilder _sessionBuilder = new HybridSessionBuilder();

        public DatabaseTesterBase()
            : base(_sessionBuilder)
        {
        }

        public static void EmptyDatabase(Database database)
        {
            using (ISession session = _sessionBuilder.GetSession(database))
            {
                using (IDbCommand command = session.Connection.CreateCommand())
                {
                    command.CommandText = "delete from attendees;delete from conferences";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void recreateDatabase(Database selectedDatabase)
        {
            SchemaExport exporter = new SchemaExport(_sessionBuilder.GetConfiguration(selectedDatabase));
            exporter.Execute(false, true, false, true);
        }
    }
}