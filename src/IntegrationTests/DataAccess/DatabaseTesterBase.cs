using CodeCampServer.DataAccess.Impl;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.DataAccess
{
	public class DatabaseTesterBase : RepositoryBase
	{
		public DatabaseTesterBase() : base(new HybridSessionBuilder())
		{
		}

		[SetUp]
		public virtual void Setup()
		{
			recreateDatabase();
		}

		public static void recreateDatabase()
		{
			var exporter = new SchemaExport(new HybridSessionBuilder().GetConfiguration());
			exporter.Execute(false, true, false, true);
		}
	}
}