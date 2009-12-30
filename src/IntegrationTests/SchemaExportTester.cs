using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Configuration=NHibernate.Cfg.Configuration;

namespace CodeCampServer.IntegrationTests
{
	[TestFixture(Description = "SchemaExport"), Explicit]
	public class SchemaExportTester
	{
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
			var tables = DatabaseDeleter.GetTables();
			foreach (var table in tables)
			{
				try
				{
					connection.ExecuteNonQuery(string.Format("drop table [{0}];", table));
				}
				catch (Exception e) {}
			}
		}

		private ServerConnection GetServerConnection(Configuration configuration)
		{
			string connectionString = GetConnectionString(configuration);
			var conn = new SqlConnection(connectionString);
			var server = new Server(new ServerConnection(conn));
			return server.ConnectionContext;
		}

		[Test, Category("SchemaExport"), Explicit]
		public void ExportSchema()
		{
			Configuration configuration = new ConfigurationFactory().Build();

			DropAllTables(configuration);

			new SchemaExport(configuration).Execute(true, true, false);

			RenameForeignKeys(configuration);
		}
	}
}