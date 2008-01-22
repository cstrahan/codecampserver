using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using Iesi.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using CodeCampServer.Model.Security;
using CodeCampServer.Model.Exceptions;

namespace CodeCampServer.UnitTests.Model
{
	[TestFixture]
	public class ConferenceServiceTester
	{
		[Test]
		public void ShouldGetConferenceByKey()
		{
			MockRepository mocks = new MockRepository();
			ILoginService loginService = mocks.CreateMock<ILoginService>();
			IConferenceRepository repository = mocks.CreateMock<IConferenceRepository>();
			Conference expectedConference = new Conference();
			SetupResult.For(repository.GetConferenceByKey("foo")).Return(expectedConference);
			mocks.ReplayAll();

            IConferenceService service = new ConferenceService(repository, null, loginService, null, null, null);
			Conference actualConference = service.GetConference("foo");

			Assert.That(actualConference, Is.EqualTo(expectedConference));
		}

		[Test]
		public void RegisteringAttendeeShouldSaveToRepository()
		{
			MockRepository mocks = new MockRepository();
			ILoginService loginService = mocks.CreateMock<ILoginService>();
			Expect.Call(loginService.CreateSalt()).Return("salt");
			Expect.Call(loginService.CreatePasswordHash("password", "salt")).Return("gobblygook");

			IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();
			Attendee actualAttendee = null;
			repository.Save(null);
			LastCall.IgnoreArguments().Do(new Action<Attendee>(delegate(Attendee obj) { actualAttendee = obj; }));
			mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, repository, loginService, null, null, null);
			Conference conference = new Conference();
			Attendee attendee = service.RegisterAttendee("fn", "ln", "w", "c", conference, "email", "password");

			mocks.VerifyAll();

			Assert.That(actualAttendee, Is.EqualTo(attendee));
			Assert.That(attendee.Password, Is.EqualTo("gobblygook"));
			Assert.That(attendee.PasswordSalt, Is.EqualTo("salt"));
			Assert.That(attendee.GetName(), Is.EqualTo("fn ln"));
			Assert.That(attendee.Website, Is.EqualTo("w"));
			Assert.That(attendee.Comment, Is.EqualTo("c"));
			Assert.That(attendee.Contact.Email, Is.EqualTo("email"));
			Assert.That(attendee.Conference, Is.EqualTo(conference));
		}

		[Test]
		public void GetAttendeesShouldUseRepositoryAndRespectPageInfo()
		{
			MockRepository mocks = new MockRepository();
			ILoginService loginService = mocks.CreateMock<ILoginService>();
			IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();
			Conference targetConference = new Conference();
			Attendee[] toReturn = new Attendee[] {new Attendee(), new Attendee()};
			SetupResult.For(repository.GetAttendeesForConference(targetConference, 2, 3)).Return(
				toReturn);
			mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, repository, loginService, null, null, null);
			IEnumerable<Attendee> attendees = service.GetAttendees(targetConference, 2, 3);
			List<Attendee> attendeesList = new List<Attendee>(attendees);

			Assert.That(attendeesList.ToArray(), Is.EqualTo(toReturn));
		}

		[Test]
		public void ShouldGetSpeakerByEmail()
		{
			MockRepository mocks = new MockRepository();
			ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();
			Conference targetConference = new Conference();
			string email = "brownie@brownie.com.au";
            string displayName = "AndrewBrowne";
			Speaker expectedSpeaker =
				new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", targetConference,
                             email, displayName, "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");
			SetupResult.For(repository.GetSpeakerByDisplayName(displayName)).Return(expectedSpeaker);
			mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, null, null, repository, null, null);
			Speaker actualSpeaker = service.GetSpeakerByDisplayName(displayName);

			Assert.That(actualSpeaker, Is.EqualTo(expectedSpeaker));
		}

        [Test]
        public void GetSpeakersShouldUseRepositoryAndRespectPageInfo()
        {
            MockRepository mocks = new MockRepository();
            ILoginService loginService = mocks.CreateMock<ILoginService>();
            ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();
            Conference targetConference = new Conference();
            Speaker[] toReturn = new Speaker[] { new Speaker(), new Speaker() };
            SetupResult.For(repository.GetSpeakersForConference(targetConference, 2, 3)).Return(
                toReturn);
            mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, null, loginService, repository, null, null);
            IEnumerable<Speaker> speakers = service.GetSpeakers(targetConference, 2, 3);
            List<Speaker> speakersList = new List<Speaker>(speakers);

            Assert.That(speakersList.ToArray(), Is.EqualTo(toReturn));
        }

        [Test]
        public void GetCurrentSpeakerProfileShouldUseAuthenticationServiceAndRepository()
        {
            MockRepository mocks = new MockRepository();

            Conference anConference = new Conference();
            Speaker expectedResult =
				new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", anConference,
                             "brownie@brownie.com.au", "AndrewBrowne", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");
			
            IAuthenticationService authService = mocks.CreateMock<IAuthenticationService>();
            ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();
            SetupResult.For(authService.GetActiveUser()).Return("brownie@brownie.com.au");
            SetupResult.For(repository.GetSpeakerByEmail("brownie@brownie.com.au")).Return(expectedResult);
            mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, null, null, repository, authService, null);
            Speaker speaker = service.GetLoggedInSpeaker();

            Assert.AreSame(expectedResult, speaker);
        }

        [Test]
        public void GetLogginInUserNameUsesAuthService()
        {
            MockRepository mocks = new MockRepository();

            IAuthenticationService authService = mocks.CreateMock<IAuthenticationService>();
            SetupResult.For(authService.GetActiveUser()).Return("username");
            mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, null, null, null, authService, null);
            string username = service.GetLoggedInUsername();

            Assert.AreEqual("username", username);
        }

        [Test]
        public void GetLoggedInSpeakerReturnsNullOnNoUser()
        {
            MockRepository mocks = new MockRepository();

            IAuthenticationService authService = mocks.CreateMock<IAuthenticationService>();
            SetupResult.For(authService.GetActiveUser()).Return("");
            mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, null, null, null, authService, null);
            Speaker speaker = service.GetLoggedInSpeaker();

            Assert.IsNull(speaker);
        }

        Speaker getSpeaker()
        {
            return new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", new Conference(),
                             "brownie@brownie.com.au", "AndrewBrowne", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");
        }

        
        [Test]
        public void SaveSpeakerSavesToSpeakerRepository()
        {
            MockRepository mocks = new MockRepository();
            ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();

            Speaker theSpeaker =
                getSpeaker();

            SetupResult.For(repository.GetSpeakerByEmail("brownie@brownie.com.au")).Return(theSpeaker);
            SetupResult.For(repository.CanSaveSpeakerWithDisplayName(theSpeaker, "UpdatedDisplayName")).Return(true);
            Speaker actualSpeaker = null;
            repository.Save(null);
            LastCall.IgnoreArguments().Do(new Action<Speaker>(delegate(Speaker obj) { actualSpeaker = obj; }));
			
            mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, null, null, repository, null, null);
            Speaker speaker = service.SaveSpeaker("brownie@brownie.com.au", "UpdatedFirstName", "UpdatedLastName", "http://updated.website", "UpdatedComment", "UpdatedDisplayName", "updated profile", "http://updated.avatar.url");
            Assert.AreEqual("UpdatedFirstName", actualSpeaker.Contact.FirstName);
            Assert.AreEqual("UpdatedLastName",actualSpeaker.Contact.LastName);
            Assert.AreEqual("http://updated.website",actualSpeaker.Website);
            Assert.AreEqual("UpdatedComment",actualSpeaker.Comment);
            Assert.AreEqual("UpdatedDisplayName",actualSpeaker.DisplayName);
            Assert.AreEqual("updated profile",actualSpeaker.Profile);
            Assert.AreEqual("http://updated.avatar.url",actualSpeaker.AvatarUrl);
        }

        [Test]
        public void SaveSpeakerThrowsExceptionIfUpdatedDisplayNameIsNotUnique()
        {
            MockRepository mocks = new MockRepository();
            ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();

            Speaker theSpeaker = getSpeaker();

            SetupResult.For(repository.GetSpeakerByEmail("brownie@brownie.com.au")).Return(theSpeaker);
            SetupResult.For(repository.CanSaveSpeakerWithDisplayName(theSpeaker, "UpdatedDisplayName")).Return(false);
            
            mocks.ReplayAll();

            DataValidationException exception = null;
            IConferenceService service = new ConferenceService(null, null, null, repository, null, null);
            try
            {
                service.SaveSpeaker("brownie@brownie.com.au", "UpdatedFirstName", "UpdatedLastName", "http://updated.website", "UpdatedComment", "UpdatedDisplayName", "updated profile", "http://updated.avatar.url");
            }
            catch (DataValidationException ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
            Assert.AreEqual("DisplayName is already in use", exception.Message);
        }

		//TODO:  Please avoid using C# 3.0 features until Resharper 4.0 comes our (20 days).
        [Test]
        public void CreatingNewSessionShouldSaveSessionToRepository()
        {
            MockRepository mocks = new MockRepository();

            ISessionRepository repository = mocks.CreateMock<ISessionRepository>();
            Session actualSession = null;
            repository.Save(null);
            LastCall.IgnoreArguments().Do(new Action<Session>(delegate(Session obj) { actualSession = obj; }));
            mocks.ReplayAll();

            IConferenceService service = new ConferenceService(null, null, null, null, null, repository);
            Speaker speaker = new Speaker("a", "b", "c", "d", new Conference(), "e", "f", "g", "h", "password", "salt");
            ISet<OnlineResource> resources = new HashedSet<OnlineResource>
            {
                new OnlineResource { Name = "Name", Type = OnlineResourceType.Blog, Href = "http://myblog.com" }
            };
            Session session = service.CreateSession(speaker, "title", "abstract", resources);

            mocks.VerifyAll();

            Assert.That(actualSession, Is.EqualTo(session));
            Assert.That(actualSession.Speaker, Is.EqualTo(speaker));
            Assert.That(actualSession.Title, Is.EqualTo("title"));
            Assert.That(actualSession.Abstract, Is.EqualTo("abstract"));
            Assert.That(actualSession.Resources, Is.EqualTo(resources));
        }
	}
}
