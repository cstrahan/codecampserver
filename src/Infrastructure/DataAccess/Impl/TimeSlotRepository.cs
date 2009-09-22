using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NHibernate;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class TimeSlotRepository : RepositoryBase<TimeSlot>, ITimeSlotRepository
	{
		public TimeSlotRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}

		public TimeSlot[] GetAllForConference(Conference conference)
		{
			ISession session = GetSession();
			IQuery query = session.CreateQuery("from TimeSlot t where t.Conference = ? order by t.StartTime");
			query.SetParameter(0, conference);
			IList<TimeSlot> list = query.List<TimeSlot>();
			return list.ToArray();			
		}
	}
}