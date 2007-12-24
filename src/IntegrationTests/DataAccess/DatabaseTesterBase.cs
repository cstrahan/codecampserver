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

        public static void recreateDatabase(Database selectedDatabase)
        {
            SchemaExport exporter = new SchemaExport(_sessionBuilder.GetConfiguration(selectedDatabase));
            exporter.Execute(false, true, false, true);
        }

        protected void resetSession(Database database)
        {
            HybridSessionBuilder.ResetSession(database);
        }
    }
}