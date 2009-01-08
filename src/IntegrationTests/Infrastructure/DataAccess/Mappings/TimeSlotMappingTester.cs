using System;
using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	public class TimeSlotMappingTester : DataTestBase
	{
		[Test]
		public void Should_map_TimeSlot()
		{
			var conference = new Conference {StartDate = new DateTime(2001, 1, 1), EndDate = new DateTime(2001, 1, 2)};
			var timeSlot = new TimeSlot
			               	{Conference = conference, StartTime = new DateTime(2001, 1, 1), EndTime = new DateTime(2002, 2, 1)};
			PersistEntity(conference);
			AssertObjectCanBePersisted(timeSlot);
		}
	}
}