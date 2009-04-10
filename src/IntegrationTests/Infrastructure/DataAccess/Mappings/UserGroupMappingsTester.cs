using System;
using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class UserGroupMappingsTester : DataTestBase
	{
		[Test]
		public void Should_map_user_group()
		{
		    var usergroup = new UserGroup();
		    usergroup.Name = "this group";
		    usergroup.Key = "AustinDotNetUsersGroup";
		    usergroup.City = "city";
		    usergroup.Region = "Stete";
		    usergroup.Country = "USA";
		    usergroup.HomepageHTML = "<H1>Hello World</H1>";
		    usergroup.GoogleAnalysticsCode = "foo";
		    usergroup.DomainName = "foo/bar";
		    usergroup.Add(new User(){EmailAddress = "foo",Name = "bar"});
		    usergroup.Add(new Sponsor(){Level = SponsorLevel.Platinum,Name = "the sponsor"});
            AssertObjectCanBePersisted(usergroup.GetSponsors()[0]);
            AssertObjectCanBePersisted(usergroup.GetUsers()[0]);
			AssertObjectCanBePersisted(usergroup);
		}
	}
}