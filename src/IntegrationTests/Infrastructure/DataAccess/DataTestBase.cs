using System;
using System.Linq;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Planning;
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
            var types =
                typeof (User).Assembly.GetTypes().Where(
                    type => type.BaseType==typeof(KeyedObject) || type.BaseType == typeof (PersistentObject) && !type.IsAbstract).ToArray();
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