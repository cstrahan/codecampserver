using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class TimeSlotUpdaterTester : TestBase
	{
		[Test]
		public void Should_save_a_new_timeslot_from_message()
		{
			var message = S<ITimeSlotMessage>();
			message.StartTime = new DateTime(2009,5,30,9,0,0).ToString();
			message.EndTime = new DateTime(2009, 5, 30, 10, 30, 0).ToString();
			message.ConferenceId = Guid.NewGuid();
			message.Id = Guid.Empty;
			var conference = new Conference();
			


			var repository = M<ITimeSlotRepository>();
			repository.Stub(x => x.GetById(message.Id)).Return(null);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(message.ConferenceId)).Return(conference);

			ITimeSlotUpdater updater = new TimeSlotUpdater(repository, conferenceRepository);

			UpdateResult<TimeSlot, ITimeSlotMessage> result = updater.UpdateFromMessage(message);

			result.Successful.ShouldBeTrue();
			TimeSlot modelTimeSlot = result.Model;
			modelTimeSlot.StartTime.ShouldEqual(new DateTime(2009, 5, 30, 9, 0, 0));
			modelTimeSlot.EndTime.ShouldEqual(new DateTime(2009, 5, 30, 10,30, 0));
			modelTimeSlot.Conference.ShouldEqual(conference);

			repository.AssertWasCalled(r=>r.Save(modelTimeSlot));
		}

		[Test]
		public void Should_update_a_timeslot_from_message()
		{
			var message = S<ITimeSlotMessage>();
			message.StartTime = new DateTime(2009, 5, 30, 9, 0, 0).ToString();
			message.EndTime = new DateTime(2009, 5, 30, 10, 30, 0).ToString();
			message.ConferenceId = Guid.NewGuid();
			message.Id = Guid.Empty;
			var conference = new Conference();
			var timeSlot = new TimeSlot();


			var repository = M<ITimeSlotRepository>();
			repository.Stub(x => x.GetById(message.Id)).Return(timeSlot);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(message.ConferenceId)).Return(conference);

			ITimeSlotUpdater updater = new TimeSlotUpdater(repository, conferenceRepository);

			UpdateResult<TimeSlot, ITimeSlotMessage> result = updater.UpdateFromMessage(message);

			result.Successful.ShouldBeTrue();
			result.Model.ShouldEqual(timeSlot);
			TimeSlot modelTimeSlot = result.Model;
			modelTimeSlot.StartTime.ShouldEqual(new DateTime(2009, 5, 30, 9, 0, 0));
			modelTimeSlot.EndTime.ShouldEqual(new DateTime(2009, 5, 30, 10, 30, 0));
			modelTimeSlot.Conference.ShouldEqual(conference);

			repository.AssertWasCalled(r => r.Save(modelTimeSlot));
		}

		[Test]
		public void Should_validate_time_formats_in_the_message()
		{
			var message = S<ITimeSlotMessage>();
			message.StartTime = "asdfd";
			message.EndTime = "234234";
			message.ConferenceId = Guid.NewGuid();
			message.Id = Guid.Empty;
			var conference = new Conference();

			var repository = M<ITimeSlotRepository>();
			repository.Stub(x => x.GetById(message.Id)).Return(null);

			var conferenceRepository = M<IConferenceRepository>();
			conferenceRepository.Stub(x => x.GetById(message.ConferenceId)).Return(conference);

			ITimeSlotUpdater updater = new TimeSlotUpdater(repository, conferenceRepository);

			UpdateResult<TimeSlot, ITimeSlotMessage> result = updater.UpdateFromMessage(message);

			result.Successful.ShouldBeFalse();
			result.GetMessages(m=>m.StartTime).Length.ShouldEqual(1);
			result.GetMessages(m => m.EndTime).Length.ShouldEqual(1);
		}
	}
}