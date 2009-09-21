using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaExport"), Explicit]
	public class SchemaExportTester
	{
		[Test, Category("SchemaExport"),Explicit]
		public void ExportSchema()
		{
			new SchemaExport(getSessionBuilder().GetConfiguration())
				.Create(true, true);
		}

		private static HybridSessionBuilder getSessionBuilder()
		{
			return new HybridSessionBuilder();
		}
	}
}