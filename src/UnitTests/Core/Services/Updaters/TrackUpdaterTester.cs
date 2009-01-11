using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class TrackUpdaterTester : TestBase
	{
		[Test]
		public void Should_save_new_track_from_message()
		{
			var message = S<TrackForm>();
			message.Name = "name";
			message.ConferenceId = Guid.NewGuid();
			message.Id = Guid.Empty;
			var conference = new Conference();

			var repository = M<ITrackRepository>();
			repository.Stub(x => x.GetById(message.Id)).Return(null);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(message.ConferenceId)).Return(conference);

			ITrackUpdater updater = new TrackUpdater(repository, conferenceRepository);

			UpdateResult<Track, TrackForm> result = updater.UpdateFromMessage(message);

			result.Successful.ShouldBeTrue();
			Track track = result.Model;
			track.Name.ShouldEqual("name");
			track.Conference.ShouldEqual(conference);
		}

		[Test]
		public void Should_update_track_from_message()
		{
			var message = S<TrackForm>();
			message.Name = "name";
			message.ConferenceId = Guid.NewGuid();
			message.Id = Guid.NewGuid();

			var conference = new Conference();
			var track = new Track();

			var repository = M<ITrackRepository>();

			repository.Stub(x => x.GetById(message.Id)).Return(track);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(message.ConferenceId)).Return(conference);

			ITrackUpdater updater = new TrackUpdater(repository, conferenceRepository);

			UpdateResult<Track, TrackForm> result = updater.UpdateFromMessage(message);

			result.Successful.ShouldBeTrue();
			result.Model.ShouldEqual(track);
			track.Conference.ShouldEqual(conference);
			track.Name.ShouldEqual("name");
		}
	}
}