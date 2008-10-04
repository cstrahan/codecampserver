using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using NHibernate;
using System.Linq;

namespace CodeCampServer.DataAccess.Impl
{
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        public SessionRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        public void Save(Session session)
        {
            ISession dataSession = getSession();
            ITransaction transaction = dataSession.BeginTransaction();
            dataSession.SaveOrUpdate(session);
            transaction.Commit();
        }

        public Session[] GetProposedSessions(Conference conference)
        {
            IQuery query = getSession().CreateQuery(
                @"from Session s join fetch s.Conference 
                 where s.Conference = ? and s.IsApproved = false");
            query.SetParameter(0, conference, NHibernateUtil.Entity(typeof(Conference)));
            return new List<Session>(query.List<Session>()).ToArray();
        }

        // TODO: make this one HQL query
        public Session[] GetUnallocatedApprovedSessions(Conference conference)
        {
            IQuery allocatedSessionsQuery = getSession().CreateQuery(
                @" select s
                    from TimeSlot t inner join t.Sessions as s");
            var allocatedSessions = new List<Session>(allocatedSessionsQuery.List<Session>());

            IQuery query = getSession().CreateQuery(
                @"from Session s join fetch s.Conference 
                 where s.Conference = ? and s.IsApproved = true");
            query.SetParameter(0, conference, NHibernateUtil.Entity(typeof(Conference)));

            var approvedSessions = query.List<Session>();
            return approvedSessions.Where(session => !allocatedSessions.Contains(session)).ToArray();
        }
    }
}
