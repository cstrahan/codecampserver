using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using NHibernate;
using NHibernate.Expression;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class AttendeeRepository : RepositoryBase, IAttendeeRepository
	{
		public AttendeeRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		public IEnumerable<Attendee> GetAttendeesForConference(Conference anConference)
		{
			ISession session = getSession();

			IQuery query = session.CreateQuery("from Attendee a join fetch a.Conference where a.Conference = ?");
			query.SetParameter(0, anConference, NHibernateUtil.Entity(typeof (Conference)));
			IList<Attendee> attendees = query.List<Attendee>();
			return attendees;
		}

		public void Save(Attendee attendee)
		{
			ISession session = getSession();
			ITransaction transaction = session.BeginTransaction();
			session.SaveOrUpdate(attendee);
			transaction.Commit();
		}

		public Attendee[] GetAttendeesForConference(Conference conference, int pageNumber, int perPage)
		{
			ISession session = getSession();
			IQuery query =
				session.CreateQuery(
					@"from Attendee a join fetch a.Conference where a.Conference = ?
					order by a.Contact.LastName, a.Contact.FirstName");
			query.SetParameter(0, conference, NHibernateUtil.Entity(typeof (Conference)));
			query.SetMaxResults(perPage);
			query.SetFirstResult((pageNumber - 1)*perPage);
			IList<Attendee> attendees = query.List<Attendee>();
			return new List<Attendee>(attendees).ToArray();
		}

		public Attendee GetAttendeeByEmail(string email)
		{
			ISession session = getSession();
			ICriteria criteria = session.CreateCriteria(typeof (Attendee));
			criteria.Add(new EqExpression("Contact.Email", email));
			criteria.SetFetchMode("Conference", FetchMode.Eager);
			Attendee attendee = criteria.UniqueResult<Attendee>();

			return attendee;
		}
	}
}