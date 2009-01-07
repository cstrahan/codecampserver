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
    public class PotentialAttendeeRepository : RepositoryBase, IPotentialAttendeeRepository
    {
        public PotentialAttendeeRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        public void Save(PotentialAttendee potentialAttendee)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            session.SaveOrUpdate(potentialAttendee);
            transaction.Commit();
        }

        public PotentialAttendee GetById(Guid id)
        {
            return GetSession().Load<PotentialAttendee>(id);
        }

        public PotentialAttendee[] GetAll()
        {
            ISession session = GetSession();
            IQuery query = session.CreateQuery("from PotentialAttendee");
            return query.List<PotentialAttendee>().ToArray();
        }

    }
}