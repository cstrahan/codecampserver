using CodeCampServer.Core.Domain.Model;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.Core.Domain.Model
{
	[TestFixture]
	public class UserTester : TestBase
	{
		[Test]
		public void Should_be_admin_if_username_matches()
		{
			new User{Username = User.ADMIN_USERNAME}.IsAdmin().ShouldBeTrue();
		}
	}
}