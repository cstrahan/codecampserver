using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.Infrastructure.DataAccess.Impl
{
	public class TrackRepository : RepositoryBase<Track>, ITrackRepository
	{
		public TrackRepository(ISessionBuilder sessionFactory) : base(sessionFactory)
		{
		}
	}
}