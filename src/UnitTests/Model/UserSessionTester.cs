using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Model
{
	[TestFixture]
	public class UserSessionTester
	{
		[Test]
		public void ShouldGetCurrentUser()
		{
			Attendee currentUser = new Attendee();
			MockRepository mocks = new MockRepository();
			IAuthenticationService service = mocks.CreateMock<IAuthenticationService>();
			IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();
			SetupResult.For(service.GetActiveUserName()).Return("foo");
			SetupResult.For(repository.GetAttendeeByEmail("foo")).Return(currentUser);
			mocks.ReplayAll();

			IUserSession userSession = new UserSession(service, repository);
			Attendee actualUser = userSession.GetCurrentUser();
			Assert.That(actualUser, Is.EqualTo(currentUser));
		}
	}
}