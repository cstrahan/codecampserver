using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	public class SponsorMappingsTester : DataTestBase
	{
		[Test]
		public void Should_map_a_sponsor()
		{
			var userGroup = new UserGroup();
			var sponsor = new Sponsor
			              	{
			              		Name = "FooBar",
                                Level =SponsorLevel.Gold,
                                Url = "http://thisistheurl.com/",
																UserGroup = userGroup
			              	};

			PersistEntities(userGroup);

			AssertObjectCanBePersisted(sponsor);

		}
	}
}