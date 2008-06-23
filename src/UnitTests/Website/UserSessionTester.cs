using System.Security.Principal;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website;
using CodeCampServer.Website.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website
{
	[TestFixture]
	public class UserSessionTester
	{
		[Test]
		public void ShouldGetCurrentUser()
		{
			var currentUser = new Person();
			var authenticatorStub = MockRepository.GenerateStub<IAuthenticator>();
			var repositoryStub = MockRepository.GenerateStub<IPersonRepository>();
			authenticatorStub.Stub(r=>r.GetActiveIdentity()).Return(new GenericIdentity("foo"));
			repositoryStub.Stub(r=>r.FindByEmail("foo")).Return(currentUser);

			IUserSession userSession = new UserSession(authenticatorStub, repositoryStub, null);
			Person actualUser = userSession.GetLoggedInPerson();
			Assert.That(actualUser, Is.EqualTo(currentUser));
		}

		[Test]
		public void GetLoggedInSpeakerReturnsNullOnNoUser()
		{
			var authenticatorStub = MockRepository.GenerateStub<IAuthenticator>();
			var repositoryStub = MockRepository.GenerateStub<IPersonRepository>();

			IUserSession userSession = new UserSession(authenticatorStub, repositoryStub, null);
			authenticatorStub.Stub(r => r.GetActiveIdentity()).Return(new GenericIdentity(""));
			repositoryStub.Stub(r => r.FindByEmail("foo")).Return(null);

			Assert.IsNull(userSession.GetLoggedInPerson());
		}

		[Test]
		public void Should_return_isadministrator_of_logged_in_person()
		{
			var currentUser = new Person() {IsAdministrator = true};
			var authenticatorStub = MockRepository.GenerateStub<IAuthenticator>();
			var repositoryStub = MockRepository.GenerateStub<IPersonRepository>();
			authenticatorStub.Stub(r => r.GetActiveIdentity()).Return(new GenericIdentity("foo"));
			repositoryStub.Stub(r => r.FindByEmail("foo")).Return(currentUser);

			IUserSession userSession = new UserSession(authenticatorStub, repositoryStub, null);
			bool isAdministrator = userSession.IsAdministrator;
			Assert.That(isAdministrator);
		}

		[Test]
		public void Should_push_and_pop_user_messages()
		{
			var httpContextProviderStub = MockRepository.GenerateStub<IHttpContextProvider>();
			var stateStub = new HttpSessionStateStub();
			httpContextProviderStub.Stub(stub => stub.GetHttpSession()).Return(stateStub).Repeat.Any();
			IUserSession userSession = new UserSession(null, null, httpContextProviderStub);

			userSession.PushUserMessage(FlashMessage.MessageType.Error, "1");
			userSession.PushUserMessage(FlashMessage.MessageType.Error, "2");
			userSession.PushUserMessage(FlashMessage.MessageType.Message, "3");

			FlashMessage[] messages = userSession.PopUserMessages();
			Assert.That(messages[0].Type, Is.EqualTo(FlashMessage.MessageType.Message));
			Assert.That(messages[0].Message, Is.EqualTo("3"));

			Assert.That(messages[1].Type, Is.EqualTo(FlashMessage.MessageType.Error));
			Assert.That(messages[1].Message, Is.EqualTo("2"));

			Assert.That(messages[2].Type, Is.EqualTo(FlashMessage.MessageType.Error));
			Assert.That(messages[2].Message, Is.EqualTo("1"));
		}
	}
}