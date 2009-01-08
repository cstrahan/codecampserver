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

        public void Save(Attendee attendee)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            session.SaveOrUpdate(attendee);
            transaction.Commit();
        }

        public Attendee GetById(Guid id)
        {
            return GetSession().Load<Attendee>(id);
        }

        public Attendee[] GetAll()
        {
            ISession session = GetSession();
            IQuery query = session.CreateQuery("from Attendee");
            return query.List<Attendee>().ToArray();
        }

        public Attendee GetByEmail(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}