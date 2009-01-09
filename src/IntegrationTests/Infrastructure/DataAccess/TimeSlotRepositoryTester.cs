using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
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

			var timeSlot = new TimeSlot() { Conference = conference };
			var timeSlot1 = new TimeSlot { Conference = conference };
			var timeSlot2 = new TimeSlot { Conference = conference2 };

			PersistEntities(conference, conference2, timeSlot, timeSlot1, timeSlot2);

			ITimeSlotRepository repository = CreateRepository();
			TimeSlot[] timeSlots = repository.GetAllForConference(conference);
			CollectionAssert.Contains(timeSlots, timeSlot);
			CollectionAssert.Contains(timeSlots, timeSlot1);
			CollectionAssert.DoesNotContain(timeSlots, timeSlot2);
		}
	}
}