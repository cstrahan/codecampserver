using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Common;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using NHibernate;
using NHibernate.Transform;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class DatabaseDeleter
	{
		private readonly ISessionBuilder _builder;

		private static readonly string[] _ignoredTables = new[]
		                                                  	{
		                                                  		"conference_migration", "sysdiagrams", "usd_AppliedDatabaseScript"
		                                                  	};

		private static string[] _tablesToDelete;
		private static string _deleteSql;
		private static readonly object _lockObj = new object();
		private static bool _initialized;

		private class Relationship
		{
			public string PrimaryKeyTable;
			public string ForeignKeyTable;
		}

		public DatabaseDeleter(ISessionBuilder builder)
		{
			_builder = builder;

			BuildDeleteTables();
		}

		public virtual void DeleteAllObjects()
		{
			var session = _builder.GetSession();
			using (var command = session.Connection.CreateCommand())
			{
				command.CommandText = _deleteSql;
				command.ExecuteNonQuery();
			}
		}

		public string[] GetTables()
		{
			return _tablesToDelete;
		}

		private void BuildDeleteTables()
		{
			if (!_initialized)
			{
				lock (_lockObj)
				{
					if (!_initialized)
					{
						var session = _builder.GetSession();

						var allTables = GetAllTables(session);

						var allRelationships = GetRelationships(session);

						_tablesToDelete = BuildTableList(allTables, allRelationships);

						_deleteSql = BuildTableSql(_tablesToDelete);
//						Console.WriteLine(_deleteSql);

						_initialized = true;
					}
				}
			}
		}

		private string BuildTableSql(string[] tablesToDelete)
		{
			var completeQuery = "";
			foreach (var tableName in tablesToDelete)
			{
				completeQuery += string.Format("delete from [{0}];", tableName);
			}
			return completeQuery;
		}

		private string[] BuildTableList(ICollection<string> allTables, ICollection<Relationship> allRelationships)
		{
			var tablesToDelete = new List<string>();

			while (allTables.Any())
			{
				var leafTables = allTables.Except(allRelationships.Select(rel => rel.PrimaryKeyTable)).ToArray();

				tablesToDelete.AddRange(leafTables);

				leafTables.ForEach(lt =>
				                   	{
				                   		allTables.Remove(lt);
				                   		var relToRemove = allRelationships.Where(rel => rel.ForeignKeyTable == lt).ToArray();
				                   		relToRemove.ForEach(toRemove => allRelationships.Remove(toRemove));
				                   	});
			}

			return tablesToDelete.ToArray();
		}

		private IList<Relationship> GetRelationships(ISession session)
		{
			var otherquery =
				session.CreateSQLQuery(
					@"select
	so_pk.name as PrimaryKeyTable
,   so_fk.name as ForeignKeyTable
from
	sysforeignkeys sfk
	  inner join sysobjects so_pk on sfk.rkeyid = so_pk.id
	  inner join sysobjects so_fk on sfk.fkeyid = so_fk.id
order by
	so_pk.name
,   so_fk.name");

			return otherquery.SetResultTransformer(Transformers.AliasToBean<Relationship>()).List<Relationship>();
		}

		private IList<string> GetAllTables(ISession session)
		{
			var query = session.CreateSQLQuery("select name from sys.tables");

			return query.List<string>().Except(_ignoredTables).ToList();
		}
	}
}