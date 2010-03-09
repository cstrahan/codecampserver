using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Core.Services
{
	[TestFixture]
	public class AuthenticationServiceTester : TestBase
	{
		[Test]
		public void Should_authenticate_if_salt_matches()
		{
			var user = new User {PasswordHash = "123xyz"};
			var cryptographer = S<ICryptographer>();
			cryptographer.Stub(x => x.GetPasswordHash("password", user.PasswordSalt)).Return("123xyz");
			cryptographer.Stub(x => x.GetPasswordHash("pasword", user.PasswordSalt)).Return("123xy");

			IAuthenticationService service = new AuthenticationService(cryptographer);

			service.PasswordMatches(user, "password").ShouldBeTrue();
			service.PasswordMatches(user, "pasword").ShouldBeFalse();
		}
	}
}