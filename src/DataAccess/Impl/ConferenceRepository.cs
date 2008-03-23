using System;
using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using NHibernate;

namespace CodeCampServer.DataAccess.Impl
{
	public class ConferenceRepository : RepositoryBase, IConferenceRepository
	{
		public ConferenceRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		public IEnumerable<Conference> GetAllConferences()
		{
			ISession session = getSession();
			IQuery query = session.CreateQuery("from Conference");

			IEnumerable<Conference> result = query.List<Conference>();

			return result;
		}

		public Conference GetConferenceByKey(string key)
		{
			ISession session = getSession();
			string hql =
				@"from Conference c where c.Key = ?";
			IQuery query = session.CreateQuery(hql);
			query.SetParameter(0, key);
			Conference result = query.UniqueResult<Conference>();
            if(result == null)
            {
                throw new ObjectNotFoundException(key, typeof(Conference));
            }

			return result;
		}

		public Conference GetFirstConferenceAfterDate(DateTime date)
		{
			ISession session = getSession();
			IQuery query = session.CreateQuery("from Conference e where e.StartDate >= ? order by e.StartDate asc");
			query.SetParameter(0, date);
			query.SetMaxResults(1);
			Conference matchingConference = query.UniqueResult<Conference>();
			return matchingConference;
		}

        public Conference GetMostRecentConference(DateTime date)
        {
            ISession session = getSession();
            IQuery query = session.CreateQuery("from Conference c where c.EndDate <= ? order by c.EndDate desc");
            query.SetParameter(0, date);
            query.SetMaxResults(1);
            return query.UniqueResult<Conference>();
        }

		public Conference GetById(Guid id)
		{
			ISession session = getSession();
			return session.Get<Conference>(id);
		}

		public void Save(Conference conference)
		{
			ISession session = getSession();
			session.SaveOrUpdate(conference);
			session.Flush();
		}
	}
}
