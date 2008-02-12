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
			IAttendeeRepository attendeeRepository = mocks.CreateMock<IAttendeeRepository>();
            SetupResult.For(service.GetActiveUserName()).Return("foo");
			SetupResult.For(attendeeRepository.GetAttendeeByEmail("foo")).Return(currentUser);
			mocks.ReplayAll();

            IUserSession userSession = new UserSession(service, attendeeRepository, null);
			Attendee actualUser = userSession.GetCurrentUser();
			Assert.That(actualUser, Is.EqualTo(currentUser));
		}

        [Test]
        public void GetLoggedInSpeakerReturnsNullOnNoUser()
        {
            MockRepository mocks = new MockRepository();
            IAuthenticationService authService = mocks.CreateMock<IAuthenticationService>();
            IAttendeeRepository attendeeRepository = mocks.CreateMock<IAttendeeRepository>();

            IUserSession userSession = new UserSession(authService, attendeeRepository, null);
            SetupResult.For(authService.GetActiveUserName())
                .Return(null);
            SetupResult.For(attendeeRepository.GetAttendeeByEmail(null))
                .Return(null);
            mocks.ReplayAll();

            Assert.IsNull(userSession.GetLoggedInSpeaker());
        }

    }
}