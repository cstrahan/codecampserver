using System;
using System.Collections.Generic;
using CodeCampServer.DataAccess.Impl;
using CodeCampServer.Model.Domain;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
    [Pluggable("Default")]
    public class ConferenceRepository : RepositoryBase, IConferenceRepository
    {
        public ConferenceRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Conference> GetAllConferences()
        {
            using (ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Conference");

                IEnumerable<Conference> result = query.List<Conference>();

                return result;
            }
        }

        public Conference GetConferenceByKey(string key)
        {
            using (ISession session = getSession())
            {
                string hql = @"from Conference c left join fetch c.TimeSlots ts
                    left join fetch ts.Session s left join fetch s.Speaker speaker where c.Key = ?";
                IQuery query = session.CreateQuery(hql);
                query.SetParameter(0, key);
                Conference result = query.UniqueResult<Conference>();
                return result;
            }
        }

        public Conference GetFirstConferenceAfterDate(DateTime date)
        {
            using(ISession session = getSession())
            {
                IQuery query = session.CreateQuery("from Conference e where e.StartDate >= ? order by e.StartDate asc");
                query.SetParameter(0, date);
                query.SetMaxResults(1);
                Conference matchingConference = query.UniqueResult<Conference>();
                return matchingConference;
            }
        }

        public Conference GetById(Guid id)
        {            
            using(ISession session = getSession())
            {
                return session.Get<Conference>(id);
            }
        }

        public void Save(Conference conference)
        {
            using(ISession session = getSession())
            {
                session.SaveOrUpdate(conference);
                session.Flush();
            }
        }
    }
}