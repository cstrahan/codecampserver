using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaDrop"), Explicit]
	public class SchemaImportTester
	{
		[Test, Category("SchemaDrop"),Explicit]
		public void DropSchema()
		{
			new SchemaExport(getSessionBuilder().GetConfiguration()).Drop(true, true);
		}

		private static HybridSessionBuilder getSessionBuilder()
		{
			return new HybridSessionBuilder();
		}
	}
}