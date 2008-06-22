using System.Security.Principal;
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
			var currentUser = new Person();
			var mocks = new MockRepository();
			var service = mocks.CreateMock<IAuthenticator>();
			var personRepository = mocks.CreateMock<IPersonRepository>();
			SetupResult.For(service.GetActiveIdentity()).Return(new GenericIdentity("foo"));
			SetupResult.For(personRepository.FindByEmail("foo")).Return(currentUser);
			mocks.ReplayAll();

			IUserSession userSession = new UserSession(service, personRepository);
			Person actualUser = userSession.GetLoggedInPerson();
			Assert.That(actualUser, Is.EqualTo(currentUser));
		}

		[Test]
		public void GetLoggedInSpeakerReturnsNullOnNoUser()
		{
			var mocks = new MockRepository();
			var authService = mocks.CreateMock<IAuthenticator>();
			var personRepository = mocks.CreateMock<IPersonRepository>();

			IUserSession userSession = new UserSession(authService, personRepository);
			SetupResult.For(authService.GetActiveIdentity()).Return(new GenericIdentity(""));
			SetupResult.For(personRepository.FindByEmail(null)).Return(null);
			mocks.ReplayAll();

			Assert.IsNull(userSession.GetLoggedInPerson());
		}
	}
}