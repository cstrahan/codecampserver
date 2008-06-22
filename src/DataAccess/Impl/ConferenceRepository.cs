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

		public Conference[] GetAllConferences()
		{
			ISession session = getSession();
			IQuery query = session.CreateQuery("from Conference");

			IList<Conference> result = query.List<Conference>();
			return new List<Conference>(result).ToArray();
		}

		public Conference GetConferenceByKey(string key)
		{
			ISession session = getSession();
			const string hql = @"from Conference c where c.Key = :key";
			IQuery query = session.CreateQuery(hql).SetString("key", key);
			var result = query.UniqueResult<Conference>();

			return result;
		}

		public Conference GetFirstConferenceAfterDate(DateTime date)
		{
			ISession session = getSession();
			IQuery query = session.CreateQuery("from Conference e where e.StartDate >= ? order by e.StartDate asc");
			query.SetParameter(0, date);
			query.SetMaxResults(1);
			var matchingConference = query.UniqueResult<Conference>();
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

		public bool ConferenceExists(string name, string key)
		{
			ISession session = getSession();
			IQuery query = session.CreateQuery(
				"select count(*) from Conference c where c.Name like :name OR c.Key like :key");
			query.SetString("name", name);
			query.SetString("key", key);

			query.SetMaxResults(1);

			object result = query.UniqueResult();

			return (long) result > 0;
		}

		public bool ConferenceKeyAvailable(string key)
		{
			ISession session = getSession();
			IQuery query = session.CreateQuery("select count(*) from Conference c where c.Key like :key");
			query.SetString("key", key);

			object result = query.UniqueResult();
			return (long) result == 0;
		}
	}
}