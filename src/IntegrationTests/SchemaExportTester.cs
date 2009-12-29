using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaExport"), Explicit]
	public class SchemaExportTester
	{
		[Test, Category("SchemaExport"), Explicit]
		public void ExportSchema()
		{
			new SchemaExport(new ConfigurationFactory().Build())
				.Create(true, true);
		}
	}
}