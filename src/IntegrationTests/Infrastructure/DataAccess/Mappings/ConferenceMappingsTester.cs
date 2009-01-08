using System;
using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class ConferenceMappingsTester : DataTestBase
	{
		[Test]
		public void Should_map_user()
		{
			var conference = new Conference
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
			                 		PhoneNumber = "512-555-1234",
			                 	};
			conference.AddAttendee(new Attendee
			                       	{
			                       		EmailAddress = "asdfasd",
			                       		FirstName = "first name",
			                       		LastName = "last name",
			                       		Status = AttendanceStatus.NotAttending
			                       	});

			AssertObjectCanBePersisted(conference.GetAttendees()[0]);
			AssertObjectCanBePersisted(conference);
		}
	}
}