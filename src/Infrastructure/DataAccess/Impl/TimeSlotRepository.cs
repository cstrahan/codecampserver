using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class TimeSlotRepository : RepositoryBase<TimeSlot>, ITimeSlotRepository
	{
		public TimeSlotRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}
	}
}