using System;
using System.Linq;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.DataAccess;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class DataTestBase : PersistanceSpecificationHelper
	{
		#region Setup/Teardown
		[TearDown]
		public virtual void TearDown()
		{
			GetSession().Flush();
		}
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
					type => typeof (PersistentObject).IsAssignableFrom(type) && !type.IsAbstract)
					.OrderBy(type => type.Name).ToArray();
			using (ISession session = GetSession())
			{
				session.Transaction.Begin();
				foreach (Type type in types)
				{
					
					session.Delete("from " + type.Name + " o");
				}
				session.Flush();
				session.Transaction.Commit();
				
			}
		}

		protected void PersistEntities(params PersistentObject[] entities)
		{
			using (ISession session = GetSession())
			{
				using (var tran = session.BeginTransaction())
				{
					foreach (PersistentObject entity in entities)
					{
						session.SaveOrUpdate(entity);
					}					
					tran.Commit();
				}				
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

		protected static IUnitOfWork GetSessionBuilder()
		{
			var builder = new UnitOfWork(new HybridSessionBuilder());
			builder.Begin();
			return builder;
		}
	}
}