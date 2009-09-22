using System;
using System.Linq;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class DataTestBase : PersistanceSpecificationHelper
	{
		#region Setup/Teardown

		[SetUp]
		public virtual void Setup()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			//EnsureDatabaseRecreated();
			DeleteAllObjects();
		}

		#endregion

		protected DataTestBase() : base(new HybridSessionBuilder()) {}

		protected DataTestBase(ISessionBuilder builder) : base(builder) {}

		protected virtual void EnsureDatabaseRecreated()
		{
			TestHelper.EnsureDatabaseRecreated();
		}

		protected virtual void DeleteAllObjects()
		{
			Type[] types =
				typeof (User).Assembly.GetTypes().Where(
					type => typeof (PersistentObject).IsAssignableFrom(type) && !type.IsAbstract).
					ToArray();
			using (ISession session = GetSession())
			{
				foreach (Type type in types)
				{
					session.Delete("from " + type.Name + " o");
				}
				session.Flush();
			}
		}

		protected void PersistEntities(params PersistentObject[] entities)
		{
			using (ISession session = GetSession())
			{
				foreach (PersistentObject entity in entities)
				{
					session.SaveOrUpdate(entity);
				}
				session.Flush();
			}
		}

		protected void PersistEntity(PersistentObject entity)
		{
			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(entity);
				session.Flush();
			}
		}

		protected static ISessionBuilder GetSessionBuilder()
		{
			return new HybridSessionBuilder();
		}
	}
}