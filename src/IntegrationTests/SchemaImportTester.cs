using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaDrop"), Explicit]
	public class SchemaImportTester
	{
		[Test, Category("SchemaDrop"), Explicit]
		public void DropSchema()
		{
			new SchemaExport(new ConfigurationFactory().Build()).Drop(true, true);
		}
	}
}