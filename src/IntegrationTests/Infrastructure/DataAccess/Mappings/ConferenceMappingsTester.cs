using System;
using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class ConferenceMappingsTester : DataTestBase
	{
		[Test]
		public void Can_persist()
		{
			var userGroup = new UserGroup
			                	{Name = "user group"};
			var conference = new Conference
			                 	{
			                 		Name = "sdf",
			                 		Description = "description",
			                 		StartDate = new DateTime(2008, 12, 2),
			                 		EndDate = new DateTime(2008, 12, 3),
			                 		TimeZone = "CST",
			                 		LocationName = "St Edwards Professional Education Center",
			                 		Address = "12343 Research Blvd",
			                 		City = "Austin",
			                 		Region = "Tx",
			                 		PostalCode = "78234",
			                 		PhoneNumber = "512-555-1234",
			                 		HtmlContent = "<h1>This is some markup about sponsors.</h1>",
			                 		UserGroup = userGroup,
			                 		HasRegistration = true,
			                 		LocationUrl = "http://foobar"
			                 	};

			AssertObjectCanBePersisted(userGroup);
			AssertObjectCanBePersisted(conference);
		}
	}
}