using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.Core.Domain.Model
{
	[TestFixture]
	public class UserGroupTester
	{
		[Test]
		public void Should_be_default_if_url_has_localhost()
		{
			var @group = new UserGroup();
			group.DomainName = "www.codecampserver.com";
			group.IsDefault().ShouldBeTrue();
		}

		[Test]
		public void Should_add_and_remove_users()
		{
			var @group = new UserGroup();
			var child = new User();
			group.Add(child);
			group.GetUsers().ShouldEqual(new []{child});
			group.Remove(child);
			group.GetUsers().Length.ShouldEqual(0);
		}
	}
}