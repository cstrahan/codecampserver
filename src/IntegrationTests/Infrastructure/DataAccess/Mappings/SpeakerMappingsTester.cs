using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	public class SpeakerMappingsTester : DataTestBase
	{
		[Test]
		public void Should_map_Speaker()
		{
			var speaker = new Speaker
			              	{
			              		Bio = "bio",
			              		Company = "company",
			              		EmailAddress = "email",
			              		FirstName = "first",
			              		LastName = "last",
			              		JobTitle = "title",
			              		SpeakerKey = "key",
			              		WebsiteUrl = "url"
			              	};

			AssertObjectCanBePersisted(speaker);

		}
	}
}