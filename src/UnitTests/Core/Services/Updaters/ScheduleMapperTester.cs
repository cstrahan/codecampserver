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
	[TestFixture, Explicit]
	public class ScheduleMapperTester : TestBase
	{
		[Test]
		public void When_no_sessions_Should_create_empty_Schedule()
		{
			var sessionMapper = S<ISessionMapper>();
			var trackMapper = S<ITrackMapper>();
			var timeSlotMapper = S<ITimeSlotMapper>();

			var conference = new Conference();
			var track1 = new TrackForm();
			var track2 = new TrackForm();
			var timeSlotForm1 = new TimeSlotForm();
			var timeSlotForm2 = new TimeSlotForm();
			var timeSlot1 = new TimeSlot();
			var timeSlot2 = new TimeSlot();
			var timeSlots = new []{timeSlot1, timeSlot2};
			sessionMapper.Stub(m => m.Map(null)).Return(new SessionForm[0]);
			trackMapper.Stub(m => m.Map(null)).Return(new[] {track1, track2});
			timeSlotMapper.Stub(m => m.Map(timeSlots)).Return(new[] {timeSlotForm1, timeSlotForm2});

			var mapper = new ScheduleMapper(S<ISessionRepository>(), S<ITrackRepository>(), S<ITimeSlotRepository>(),
			                                sessionMapper, trackMapper, timeSlotMapper);
			ScheduleForm form = mapper.CreateScheduleForSpecificDay(conference, DateTime.Parse("1/1/2009"), timeSlots, 1);
			form.Day.ShouldEqual(1);
			form.Date.ShouldEqual("Thursday 01/01");
			form.Tracks.Length.ShouldEqual(2);
			form.Tracks[0].ShouldEqual(track1);
			form.Tracks[1].ShouldEqual(track2);
			form.TimeSlotAssignments.Length.ShouldEqual(2);
			form.TimeSlotAssignments[0].TimeSlot.ShouldEqual(timeSlotForm1);
			form.TimeSlotAssignments[1].TimeSlot.ShouldEqual(timeSlotForm2);
		}

		[Test]
		public void When_two_sessions_in_same_slot_Should_correct_structure()
		{
			var conference = new Conference();
			var sessionMapper = S<ISessionMapper>();
			var trackMapper = S<ITrackMapper>();
			var timeSlotMapper = S<ITimeSlotMapper>();
			var track2 = new Track();
			var timeSlot1 = new TimeSlot();
			var timeSlot2 = new TimeSlot();
			var timeSlots = new []{timeSlot1, timeSlot2};
			var session1 = new SessionForm(){Track = track2, TimeSlot = timeSlot2};
			var session2 = new SessionForm(){Track = track2, TimeSlot = timeSlot2};
			var trackForm1 = new TrackForm();
			var trackForm2 = new TrackForm();
			var timeSlotForm1 = new TimeSlotForm();
			var timeSlotForm2 = new TimeSlotForm();

			sessionMapper.Stub(m => m.Map(null)).Return(new []{session1, session2});
			trackMapper.Stub(m => m.Map(null)).Return(new[] { trackForm1, trackForm2 });
			timeSlotMapper.Stub(m => m.Map(timeSlots)).Return(new[] { timeSlotForm1, timeSlotForm2 });

			var mapper = new ScheduleMapper(S<ISessionRepository>(), S<ITrackRepository>(), S<ITimeSlotRepository>(),
																			sessionMapper, trackMapper, timeSlotMapper);
			ScheduleForm form = mapper.CreateScheduleForSpecificDay(conference, DateTime.Now, timeSlots, 2);
			form.Tracks.Length.ShouldEqual(2);
			form.TimeSlotAssignments.Length.ShouldEqual(2);
			form.TimeSlotAssignments[1].TrackAssignments[1].Sessions[0].ShouldEqual(session1);
			form.TimeSlotAssignments[1].TrackAssignments[1].Sessions[1].ShouldEqual(session2);
		}


        [Test]
        public void When_two_timeslots_skips_a_day_than_the_schedule_should_skip_those_days()
        {
            var conference = new Conference();
            var sessionMapper = S<ISessionMapper>();
            var trackMapper = S<ITrackMapper>();
            var timeSlotMapper = S<ITimeSlotMapper>();
            var timeSlot1 = new TimeSlot() { StartTime = new DateTime(2020, 3, 5, 3, 0, 0), EndTime = new DateTime(2020, 3, 5, 4, 0, 0) };
            var timeSlot2 = new TimeSlot() { StartTime = new DateTime(2020, 3, 8, 3, 0, 0), EndTime = new DateTime(2020, 3, 8, 4, 0, 0) };
            var timeSlots = new[] { timeSlot1, timeSlot2 };

            var repository = S<ITimeSlotRepository>();
            repository.Stub(slotRepository => slotRepository.GetAllForConference(conference)).Return(timeSlots);

            var mapper = new MapperStub(S<ISessionRepository>(), S<ITrackRepository>(), repository,
                                                                            sessionMapper, trackMapper, timeSlotMapper);
            ScheduleForm[] form =  mapper.Map(conference);
            
            form.Length.ShouldEqual(2);
        }

        public class MapperStub: ScheduleMapper
        {
            public MapperStub(ISessionRepository sessionRepository, ITrackRepository trackRepository, ITimeSlotRepository timeSlotRepository, ISessionMapper sessionMapper, ITrackMapper trackMapper, ITimeSlotMapper timeSlotMapper) : base(sessionRepository, trackRepository, timeSlotRepository, sessionMapper, trackMapper, timeSlotMapper) {}
            public override ScheduleForm CreateScheduleForSpecificDay(Conference conference, DateTime dayStart, TimeSlot[] timeSlotsInDay, int dayNumber)
            {
                return new ScheduleForm();//{TimeSlotAssignments = new TimeSlotAssignmentForm[timeSlotsInDay.Length]};
            }
        }
    
    
    }
}