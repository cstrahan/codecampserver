using System;
using System.Collections.Generic;
using CodeCampServer.Domain;
using CodeCampServer.Domain.Model;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess
{
    [Pluggable("Default")]
    public class EventRepository : RepositoryBase, IEventRepository
    {
        public IEnumerable<Event> GetAllEvents()
        {
            using (ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Event");

                IEnumerable<Event> result = query.List<Event>();

                return result;
            }
        }

        public Event GetEventByKey(string key)
        {
            using (ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Event e where e.Key = ?");
                query.SetParameter(0, key);
                Event result = query.UniqueResult<Event>();
                return result;
            }
        }

        public Event GetFirstEventAfterDate(DateTime date)
        {
            using(ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Event e where e.StartDate >= ?");
                query.SetParameter(0, date);
                query.SetMaxResults(1);
                Event matchingEvent = query.UniqueResult<Event>();
                return matchingEvent;
            }
        }
    }
}