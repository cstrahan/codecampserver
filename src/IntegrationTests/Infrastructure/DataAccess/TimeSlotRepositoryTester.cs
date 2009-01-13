using System;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class TimeSlotRepositoryTester : RepositoryTester<TimeSlot, ITimeSlotRepository>
	{
		protected override ITimeSlotRepository CreateRepository()
		{
			return new TimeSlotRepository(GetSessionBuilder());
		}
		
		[Test]
		public void Should_get_all_timeslots_for_conference()
		{
			var conference = new Conference();
			var conference2 = new Conference();

			var timeSlot = new TimeSlot() { Conference = conference, StartTime = new DateTime(2000, 2, 1)};
			var timeSlot1 = new TimeSlot { Conference = conference, StartTime = new DateTime(2000, 2, 2) };
			var timeSlot2 = new TimeSlot { Conference = conference2};

			PersistEntities(conference, conference2, timeSlot1, timeSlot, timeSlot2);

			ITimeSlotRepository repository = CreateRepository();
			TimeSlot[] timeSlots = repository.GetAllForConference(conference);
			timeSlots[0].ShouldEqual(timeSlot);
			timeSlots[1].ShouldEqual(timeSlot1);
			CollectionAssert.DoesNotContain(timeSlots, timeSlot2);
		}
	}
}