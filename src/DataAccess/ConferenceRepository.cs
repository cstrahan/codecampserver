using System;
using System.Collections.Generic;
using CodeCampServer.Domain;
using CodeCampServer.Domain.Model;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess
{
    [Pluggable("Default")]
    public class ConferenceRepository : RepositoryBase, IConferenceRepository
    {
        public IEnumerable<Conference> GetAllEvents()
        {
            using (ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Conference");

                IEnumerable<Conference> result = query.List<Conference>();

                return result;
            }
        }

        public Conference GetEventByKey(string key)
        {
            using (ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Event e where e.Key = ?");
                query.SetParameter(0, key);
                Conference result = query.UniqueResult<Conference>();
                return result;
            }
        }

        public Conference GetFirstEventAfterDate(DateTime date)
        {
            using(ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Event e where e.StartDate >= ?");
                query.SetParameter(0, date);
                query.SetMaxResults(1);
                Conference matchingConference = query.UniqueResult<Conference>();
                return matchingConference;
            }
        }
    }
}