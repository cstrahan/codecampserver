using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases;
using Microsoft.SqlServer.Management.Smo;
using NHibernate;
using User = CodeCampServer.Core.Domain.Model.User;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class DatabaseDeleter
	{
		private readonly ISessionBuilder _builder;
       
		public DatabaseDeleter(ISessionBuilder builder)
		{
			_builder = builder;
		}

		internal virtual void DeleteAllObjects()
		{
			var tables = new List<string>();
			tables.Add("usergroupadminusers");
			tables.Add("conferences");
			tables.Add("meetings");
			tables.Add("events");
			tables.Add("sponsors");
			tables.Add("usergroups");
			tables.Add("users");

			Type[] unorderedTypes =	typeof (User).Assembly.GetTypes().Where(
					type => typeof (PersistentObject).IsAssignableFrom(type) && !type.IsAbstract)
					.OrderBy(type => type.Name).ToArray();

			ISession session = _builder.GetSession();
			session.BeginTransaction();

			foreach (var table in tables)
			{
//				Console.WriteLine(table);
				session.CreateSQLQuery(string.Format("delete from {0}", table)).ExecuteUpdate();
			}

			foreach (var type in unorderedTypes)
			{
//				Console.WriteLine(type.Name);
				session.CreateQuery(string.Format("delete {0}", type.Name)).ExecuteUpdate();
			}

			session.Transaction.Commit();
		}
	}
}