using System;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.UI.Views;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class TrackMapperTester : TestBase
	{
		[Test]
		public void Should_map_new_track_from_form()
		{
			var form = S<TrackForm>();
			form.Name = "name";
			form.ConferenceId = Guid.NewGuid();
			form.Id = Guid.Empty;
			var conference = new Conference();

			var repository = M<ITrackRepository>();
			repository.Stub(x => x.GetById(form.Id)).Return(null);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(form.ConferenceId)).Return(conference);

			ITrackMapper mapper = new TrackMapper(repository, conferenceRepository);

			Track mappedTrack = mapper.Map(form);

			mappedTrack.Name.ShouldEqual("name");
			mappedTrack.Conference.ShouldEqual(conference);
		}

		[Test]
		public void Should_map_existing_track_from_form()
		{
			var form = S<TrackForm>();
			form.Name = "name";
			form.ConferenceId = Guid.NewGuid();
			form.Id = Guid.NewGuid();

			var conference = new Conference();
			var track = new Track();

			var repository = M<ITrackRepository>();
			repository.Stub(x => x.GetById(form.Id)).Return(track);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(form.ConferenceId)).Return(conference);

			ITrackMapper mapper = new TrackMapper(repository, conferenceRepository);

			Track mapped = mapper.Map(form);

			mapped.ShouldEqual(track);
			mapped.Name.ShouldEqual("name");
			mapped.Conference.ShouldEqual(conference);
		}

		[Test]
		public void Should_be_able_to_map_to_form()
        {
            var mapper = new TrackMapper(S<ITrackRepository>(), S<IConferenceRepository>());
			TrackForm form = mapper.Map(new Track(){Conference = new Conference(),Id = Guid.NewGuid(),Name = ""});
			form.ShouldNotBeNull();
		}

		[Test]
		public void Should_be_able_to_map_array_to_form()
		{
			var mapper = new TrackMapper(S<ITrackRepository>(), S<IConferenceRepository>());
			TrackForm[] forms = mapper.Map(new []{new Track(), new Track()});
			forms.ShouldNotBeNull();
			forms.Length.ShouldEqual(2);
		}
	}
}