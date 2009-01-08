using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	public class SessionMappingsTester : DataTestBase
	{
		[Test]
		public void should_map_session()
		{
			var conference = new Conference {StartDate = new DateTime(2008, 1, 1), EndDate = new DateTime(2008, 1, 2)};
			var track = new Track();
			var speaker = new Speaker();
			var timeslot = new TimeSlot {StartTime = new DateTime(2008, 1, 1), EndTime = new DateTime(2008, 1, 2)};
			var session = new Session
			              	{
			              		Abstract = "abstract",
			              		Conference = conference,
			              		Level = SessionLevel.L100,
			              		MaterialsUrl = "url",
			              		RoomNumber = "200",
			              		Key = "key",
			              		Speaker = speaker,
			              		TimeSlot = timeslot,
			              		Title = "title",
			              		Track = track
			              	};

			PersistEntities(conference, track, speaker, timeslot);
			AssertObjectCanBePersisted(session);
		}
	}
}