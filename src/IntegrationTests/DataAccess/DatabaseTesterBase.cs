using CodeCampServer.DataAccess;
using CodeCampServer.DataAccess.Impl;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.DataAccess
{
    public class DatabaseTesterBase : RepositoryBase
    {
        protected static ISessionBuilder _sessionBuilder = new HybridSessionBuilder();

        public DatabaseTesterBase()
            : base(_sessionBuilder)
        {
        }

        [SetUp]
        public virtual void Setup()
        {
            recreateDatabase(Database.Default);
        }

        public static void recreateDatabase(Database selectedDatabase)
        {
            var exporter = new SchemaExport(_sessionBuilder.GetConfiguration(selectedDatabase));
            exporter.Execute(false, true, false, true);
        }

        protected void resetSession()
        {
            HybridSessionBuilder.ResetSession(Database.Default);
        }
    }
}