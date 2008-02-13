using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using NHibernate;
using StructureMap;

namespace CodeCampServer.DataAccess.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class TimeSlotRepository : RepositoryBase, ITimeSlotRepository
	{
        public TimeSlotRepository(ISessionBuilder sessionFactory)
            : base(sessionFactory)
		{
		}

        public void Save(TimeSlot timeSlot)
        {
            ISession session = getSession();
            session.SaveOrUpdate(timeSlot);
            session.Flush();
        }

        public TimeSlot[] GetTimeSlotsFor(Conference conference)
        {
            IQuery query = getSession().CreateQuery(
                @"from TimeSlot t join fetch t.Conference 
                 where t.Conference = ?
                 order by t.StartTime");
            query.SetParameter(0, conference, NHibernateUtil.Entity(typeof(Conference)));
            return new List<TimeSlot>(query.List<TimeSlot>()).ToArray();
        }
    }
}
