using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class MeetingRepository : KeyedRepository<Meeting>, IMeetingRepository
	{
		public MeetingRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}
	}
}