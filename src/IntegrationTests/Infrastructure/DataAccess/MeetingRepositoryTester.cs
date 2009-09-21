using System;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class MeetingRepositoryTester
	{
		private static Meeting CreateMeeting()
		{
			var meeting = new Meeting
			{
				Name = "sdf",
				Description = "description",
				StartDate = new DateTime(2008, 12, 2),
				EndDate = new DateTime(2008, 12, 3),
				LocationName = "St Edwards Professional Education Center",
				Address = "12343 Research Blvd",
				City = "Austin",
				Region = "Tx",
				PostalCode = "78234",
				LocationUrl = "http://foobar",
				Topic = "topic",
				Summary = "summary",
				SpeakerName = "speakername",
				SpeakerBio = "bio",
				SpeakerUrl = "http://google.com"
			};
			return meeting;
		}

	}
}