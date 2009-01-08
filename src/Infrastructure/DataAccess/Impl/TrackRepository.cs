using CodeCampServer.Core.Domain;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class TrackRepository : RepositoryBase, ITrackRepository
	{
		public TrackRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}
	}
}