using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using NHibernate;
using NHibernate.Expression;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class SpeakerRepository : RepositoryBase, ISpeakerRepository
	{
		public SpeakerRepository(ISessionBuilder sessionFactory)
			: base(sessionFactory)
		{
		}

        public Speaker GetSpeakerByDisplayName(string displayName)
		{
			ISession session = getSession();
			ICriteria criteria = session.CreateCriteria(typeof(Speaker));
			criteria.Add(new EqExpression("DisplayName", displayName));
			criteria.SetFetchMode("Conference", FetchMode.Eager);
			Speaker speaker = criteria.UniqueResult<Speaker>();

			return speaker;
		}

        public Speaker GetSpeakerByEmail(string email)
        {
            ISession session = getSession();
            ICriteria criteria = session.CreateCriteria(typeof(Speaker));
            criteria.Add(new EqExpression("Contact.Email", email));
            criteria.SetFetchMode("Conference", FetchMode.Eager);
            Speaker speaker = criteria.UniqueResult<Speaker>();

            return speaker;
        }

		public void Save(Speaker speaker)
		{
			ISession session = getSession();
			ITransaction transaction = session.BeginTransaction();
			session.SaveOrUpdate(speaker);
			transaction.Commit();
		}

        public IEnumerable<Speaker> GetSpeakersForConference(Conference conference, int pageNumber, int perPage)
        {
            ISession session = getSession();
            IQuery query =
                session.CreateQuery(
                    @"from Speaker a join fetch a.Conference where a.Conference = ?
					order by a.Contact.LastName, a.Contact.FirstName");
            query.SetParameter(0, conference, NHibernateUtil.Entity(typeof(Conference)));
            query.SetMaxResults(perPage);
            query.SetFirstResult((pageNumber - 1) * perPage);
            IList<Speaker> speakers = query.List<Speaker>();
            return speakers;
        }

        public bool CanSaveSpeakerWithDisplayName(Speaker speaker, string newDisplayName)
        {
            ISession session = getSession();
            ICriteria criteria = session.CreateCriteria(typeof(Speaker));
            criteria.Add(new EqExpression("DisplayName", newDisplayName));
            IList<Speaker> speakers = criteria.List<Speaker>();

            if (speakers.Count == 0)
                return true;
            else if (speakers.Count == 1)
                return object.ReferenceEquals(speaker, speakers[0]);
            else
                return false;
        }
    }
}
