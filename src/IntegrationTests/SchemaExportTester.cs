using System.Data.SqlClient;
using System.IO;
using CodeCampServer.Infrastructure.NHibernate;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Configuration = NHibernate.Cfg.Configuration;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaExport"), Explicit]
	public class SchemaExportTester : DataTestBase
	{
		[Test, Category("SchemaExport"), Explicit]
		public void ExportSchema()
		{
			Configuration configuration = new ConfigurationFactory().Build();

			DropAllTables(configuration);

			new SchemaExport(configuration).Execute(true, true, false);

			RenameForeignKeys(configuration);
		}

		private void RenameForeignKeys(Configuration configuration)
		{
			ServerConnection connection = GetServerConnection(configuration);

			string sql = File.ReadAllText("RenameForeignKeys.sql");

			connection.ExecuteNonQuery(sql);
		}

		private string GetConnectionString(Configuration configuration)
		{
			return configuration.Properties["connection.connection_string"];
		}

		private void DropAllTables(Configuration configuration)
		{
			ServerConnection connection = GetServerConnection(configuration);
			string[] tables = new DatabaseDeleter(new SessionBuilder()).GetTables();

			foreach (string table in tables)
			{
				connection.ExecuteNonQuery(string.Format("drop table [{0}];", table));
			}
		}

		private ServerConnection GetServerConnection(Configuration configuration)
		{
			string connectionString = GetConnectionString(configuration);
			var conn = new SqlConnection(connectionString);
			var server = new Server(new ServerConnection(conn));
			return server.ConnectionContext;
		}
	}
}