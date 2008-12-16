using System;
using NHibernate;
using StructureMap;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace RegressionTests
{
    public class DataCreationHelper : RepositoryBase
    {
        private ISessionBuilder sessionBuilder;
        private ISession session;

        public DataCreationHelper()
            : base(ObjectFactory.GetInstance<ISessionBuilder>())
        {
            sessionBuilder = ObjectFactory.GetInstance<ISessionBuilder>();
            session = sessionBuilder.GetSessionFactory().OpenSession();
                
        }

        public IDisposable PersistEntities(params PersistentObject[] entities)
        {            
                using (session.Transaction)
                {
                    foreach (PersistentObject entity in entities)
                    {
                        
                        session.SaveOrUpdate(entity);
                    }                    
                }
                session.Flush();
            
            return new DataSelfDeletingOnDisposalContext(this, entities);
        }

        public void DeleteEntities(params PersistentObject[] entities)
        {
                for (int i = entities.Length-1; i >= 0; i--)
                {
                    PersistentObject entity = entities[i];
                    session.Delete(entity);
                }
                session.Flush();
        }
    }
}