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
	public class ScheduleMapperTester : TestBase
	{
		[Test]
		public void When_no_sessions_Should_create_empty_Schedule()
		{
			var conference = new Conference();
			var sessionRepository = S<ISessionRepository>();
			var trackRepository = S<ITrackRepository>();
			var timeSlotRepository = S<ITimeSlotRepository>();
			var sessionMapper = S<ISessionMapper>();
			var trackMapper = S<ITrackMapper>();
			var timeSlotMapper = S<ITimeSlotMapper>();
			sessionMapper.Stub(m => m.Map(null)).Return(new SessionForm[0]);
			var track1 = new TrackForm();
			var track2 = new TrackForm();
			var timeSlot1 = new TimeSlotForm();
			var timeSlot2 = new TimeSlotForm();
			trackMapper.Stub(m => m.Map(null)).Return(new[] {track1, track2});
			timeSlotMapper.Stub(m => m.Map(null)).Return(new[] {timeSlot1, timeSlot2});

			var mapper = new ScheduleMapper(sessionRepository, trackRepository, timeSlotRepository,
			                                sessionMapper, trackMapper, timeSlotMapper);
			ScheduleForm form = mapper.Map(conference);
			form.Tracks.Length.ShouldEqual(2);
			form.Tracks[0].ShouldEqual(track1);
			form.Tracks[1].ShouldEqual(track2);
			form.TimeSlotAssignments.Length.ShouldEqual(2);
			form.TimeSlotAssignments[0].TimeSlot.ShouldEqual(timeSlot1);
			form.TimeSlotAssignments[1].TimeSlot.ShouldEqual(timeSlot2);
		}

		[Test]
		public void When_two_sessions_in_same_slot_Should_correct_structure()
		{
			var conference = new Conference();
			var sessionMapper = S<ISessionMapper>();
			var trackMapper = S<ITrackMapper>();
			var timeSlotMapper = S<ITimeSlotMapper>();
			var track1 = new TrackForm();
			var track2 = new TrackForm();
			var timeSlot1 = new TimeSlotForm();
			var timeSlot2 = new TimeSlotForm();
			var session1 = new SessionForm(){Track = track2, TimeSlot = timeSlot2};
			var session2 = new SessionForm(){Track = track2, TimeSlot = timeSlot2};

			sessionMapper.Stub(m => m.Map(null)).Return(new []{session1, session2});
			trackMapper.Stub(m => m.Map(null)).Return(new[] { track1, track2 });
			timeSlotMapper.Stub(m => m.Map(null)).Return(new[] { timeSlot1, timeSlot2 });

			var mapper = new ScheduleMapper(S<ISessionRepository>(), S<ITrackRepository>(), S<ITimeSlotRepository>(),
																			sessionMapper, trackMapper, timeSlotMapper);
			ScheduleForm form = mapper.Map(conference);
			form.Tracks.Length.ShouldEqual(2);
			form.TimeSlotAssignments.Length.ShouldEqual(2);
			form.TimeSlotAssignments[1].TrackAssignments[1].Sessions[0].ShouldEqual(session1);
			form.TimeSlotAssignments[1].TrackAssignments[1].Sessions[1].ShouldEqual(session2);
		}
	}
}