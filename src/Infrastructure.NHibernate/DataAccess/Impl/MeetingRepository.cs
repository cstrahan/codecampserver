using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl
{
	public class MeetingRepository : KeyedRepository<Meeting>, IMeetingRepository
	{}
}