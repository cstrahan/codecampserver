using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class TrackRepositoryTester : RepositoryTester<Track, ITrackRepository>
	{
		protected override ITrackRepository CreateRepository()
		{
			return new TrackRepository(GetSessionBuilder());
		}
	}
}