using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class MeetingRepository : KeyedRepository<Meeting>, IMeetingRepository
	{
		public MeetingRepository(ISessionBuilder sessionFactory) : base(sessionFactory) {}
	}
}