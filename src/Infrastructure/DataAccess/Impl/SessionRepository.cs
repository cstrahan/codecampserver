using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class SessionRepository : KeyedRepository<Session>, ISessionRepository
	{
		public SessionRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		protected override string GetEntityNaturalKeyName()
		{
			return KEY_NAME;
		}

		public Session[] GetAllForConference(Conference conference)
		{
			Session[] list =
				GetSession().CreateQuery("from Session s where s.Conference = :conference").SetEntity("conference", conference).List
					<Session>().ToArray();

			return list;
		}
	}
}