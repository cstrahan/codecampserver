using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class ConferenceRepository : KeyedRepository<Conference>, IConferenceRepository
	{
		public ConferenceRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}
	}
}