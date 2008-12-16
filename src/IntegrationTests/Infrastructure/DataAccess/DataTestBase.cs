using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.DataAccess;
using NHibernate;
using NUnit.Framework;
using StructureMap;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
    [TestFixture]
    public abstract class DataTestBase : RepositoryBase
    {
        #region Setup/Teardown

        [SetUp]
        public virtual void Setup()
        {
            DependencyRegistrar.EnsureDependenciesRegistered();
            EnsureDatabaseRecreated();
            DeleteAllObjects();

            IdCacheInterceptor.ResetState();
            IdCacheInterceptor.Enabled = false;
        }

        #endregion

        protected DataTestBase() : base(ObjectFactory.GetInstance<ISessionBuilder>())
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
    }
}