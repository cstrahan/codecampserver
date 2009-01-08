using CodeCampServer.Core.Domain;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class TimeSlotRepository : RepositoryBase, ITimeSlotRepository
	{
		public TimeSlotRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}
	}
}