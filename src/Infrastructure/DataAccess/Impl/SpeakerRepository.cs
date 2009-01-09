using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class SpeakerRepository : KeyedRepository<Speaker>, ISpeakerRepository
	{
		public SpeakerRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}
	}
}