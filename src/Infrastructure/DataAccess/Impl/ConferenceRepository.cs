using System;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
    public class ConferenceRepository : RepositoryBase, IConferenceRepository
    {
        public ConferenceRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }


        public void Save(Conference conference)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            session.SaveOrUpdate(conference);
            transaction.Commit();
        }

        Conference IConferenceRepository.GetById(Guid id)
        {
            return GetSession().Load<Conference>(id);
        }

        Conference[] IConferenceRepository.GetAll()
        {
            ISession session = GetSession();
            IQuery query = session.CreateQuery("from Conference");
            return query.List<Conference>().ToArray();
        }
    }
}