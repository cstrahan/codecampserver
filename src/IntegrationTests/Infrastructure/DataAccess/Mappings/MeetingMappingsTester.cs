using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class MeetingMappingsTester : DataTestBase
	{
		[Test]
		public void Can_persist()
		{
			var userGroup = new UserGroup {Name = "user group"};
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
												TimeZone = "CST",
												UserGroup = userGroup,
			              		LocationUrl = "http://foobar",
			              		Topic = "topic",
			              		Summary = "summary",
			              		SpeakerName = "speakername",
			              		SpeakerBio = "bio",
			              		SpeakerUrl = "http://google.com",
                                Refreshments = RefreshmentType.Pizza
			              	};

			AssertObjectCanBePersisted(userGroup);
			AssertObjectCanBePersisted(meeting);
		}
	}
}