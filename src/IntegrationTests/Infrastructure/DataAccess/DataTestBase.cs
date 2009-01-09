using CodeCampServer.DependencyResolution;
using NHibernate;
using NUnit.Framework;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public abstract class DataTestBase : RepositoryBase
	{
		[SetUp]
		public virtual void Setup()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			EnsureDatabaseRecreated();
			DeleteAllObjects();
		}

		protected DataTestBase() : base(new HybridSessionBuilder())
		{
		}

		protected DataTestBase(ISessionBuilder builder) : base(builder)
		{
		}

		protected virtual void EnsureDatabaseRecreated()
		{
			TestHelper.EnsureDatabaseRecreated();
		}

		protected virtual void DeleteAllObjects()
		{
			TestHelper.DeleteAllObjects();
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