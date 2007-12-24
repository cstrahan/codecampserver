using System.Collections.Generic;
using CodeCampServer.Domain;
using CodeCampServer.Domain.Model;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess
{
    [Pluggable("Default")]
    public class AttendeeRepository : RepositoryBase, IAttendeeRepository
    {
        public IEnumerable<Attendee> GetAttendeesForEvent(Event anEvent)
        {
            using(ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Attendee a join fetch a.Event where a.Event = ?");
                query.SetParameter(0, anEvent, NHibernateUtil.Entity(typeof(Event)));
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
    }
}