using System.Linq;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class EventRepository : KeyedRepository<Event>, IEventRepository
	{
		public EventRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}

		public Event[] GetAllForUserGroup(UserGroup usergroup)
		{
			return GetSession().CreateQuery(
				"from Event e where e.UserGroup = :usergroup order by e.StartDate desc").
				SetEntity("usergroup",
				          usergroup).List<Event>().ToArray();
		}

		public Event[] GetFutureForUserGroup(UserGroup usergroup)
		{
			return GetSession().CreateQuery(
				"from Event e where e.UserGroup = :usergroup and e.EndDate >= :datetime order by e.StartDate")
				.SetEntity("usergroup", usergroup)
				.SetDateTime("datetime", SystemTime.Now().Midnight())
				.List<Event>().ToArray();
		}
	}
}