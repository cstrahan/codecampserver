using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

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