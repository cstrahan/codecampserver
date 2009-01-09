using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class TrackRepositoryTester : RepositoryTester<Track, ITrackRepository>
	{
		protected override ITrackRepository CreateRepository()
		{
			return new TrackRepository(GetSessionBuilder());
		}

		[Test]
		public void Should_get_all_tracks_for_conference()
		{
			var conference = new Conference();
			var conference2 = new Conference();

			var track = new Track {Conference = conference};
			var track2 = new Track {Conference = conference};
			var track3 = new Track {Conference = conference2};

			PersistEntities(conference, conference2, track, track2, track3);

			ITrackRepository repository = CreateRepository();
			Track[] tracks = repository.GetAllForConference(conference);
			CollectionAssert.Contains(tracks, track);
			CollectionAssert.Contains(tracks, track2);
			CollectionAssert.DoesNotContain(tracks, track3);
		}
	}
}