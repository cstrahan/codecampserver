using System.Collections.Generic;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
    [Pluggable("Default")]
    public class AttendeeRepository : RepositoryBase, IAttendeeRepository
    {
        public AttendeeRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Attendee> GetAttendeesForConference(Conference anConference)
        {
            using(ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Attendee a join fetch a.Conference where a.Conference = ?");
                query.SetParameter(0, anConference, NHibernateUtil.Entity(typeof(Conference)));
                IList<Attendee> attendees = query.List<Attendee>();
                return attendees;
            }
        }

        public void SaveAttendee(Attendee attendee)
        {
            using(ISession session = getSession())
            {
                ITransaction transaction = session.BeginTransaction();
                session.SaveOrUpdate(attendee);
                transaction.Commit();
            }
        }

    	public IEnumerable<Attendee> GetAttendeesForConference(Conference conference, int pageNumber, int perPage)
    	{
			using (ISession session = getSession())
			{
				IQuery query = session.CreateQuery(@"from Attendee a join fetch a.Conference where a.Conference = ?
					order by a.Contact.LastName, a.Contact.FirstName");
				query.SetParameter(0, conference, NHibernateUtil.Entity(typeof(Conference)));
				query.SetMaxResults(perPage);
				query.SetFirstResult((pageNumber - 1) * perPage);
				IList<Attendee> attendees = query.List<Attendee>();
				return attendees;
			}
    	}
    }
}