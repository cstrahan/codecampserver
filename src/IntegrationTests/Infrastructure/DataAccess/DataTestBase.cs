using System;
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
            EnsureDatabaseRecreated();
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
            var types = new[]
                            {
                                typeof (User),
                                typeof (UserGroup),
                                typeof (Session),
                                typeof (Track),
                                typeof (TimeSlot),
                                typeof (Speaker),
                                typeof (Attendee),
                                typeof (Conference),
                                typeof (Proposal)
                            };
            using (ISession session = GetSession())
            {
                foreach (Type type in types)
                {
                    session.Delete("from " + type.Name + " o");
                }
                session.Flush();
            }
            //TestHelper.DeleteAllObjects();
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