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
			Person currentUser = new Person();
			MockRepository mocks = new MockRepository();
			IAuthenticationService service = mocks.CreateMock<IAuthenticationService>();
			IPersonRepository personRepository = mocks.CreateMock<IPersonRepository>();
            SetupResult.For(service.GetActiveUserName()).Return("foo");
			SetupResult.For(personRepository.FindByEmail("foo")).Return(currentUser);
			mocks.ReplayAll();

            IUserSession userSession = new UserSession(service, personRepository);
		    Person actualUser = userSession.GetLoggedInPerson();
			Assert.That(actualUser, Is.EqualTo(currentUser));
		}

        [Test]
        public void GetLoggedInSpeakerReturnsNullOnNoUser()
        {
            MockRepository mocks = new MockRepository();
            IAuthenticationService authService = mocks.CreateMock<IAuthenticationService>();
            IPersonRepository personRepository = mocks.CreateMock<IPersonRepository>();

            IUserSession userSession = new UserSession(authService, personRepository);
            SetupResult.For(authService.GetActiveUserName()).Return(null);
            SetupResult.For(personRepository.FindByEmail(null)).Return(null);
            mocks.ReplayAll();

            Assert.IsNull(userSession.GetLoggedInPerson());
        }

    }
}