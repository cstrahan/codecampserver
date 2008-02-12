using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Exceptions;
using CodeCampServer.Model.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Model
{
    [TestFixture]
    public class SpeakerServiceTester
    {
        [Test]
        public void ShouldGetSpeakerByDisplayName()
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

            ISpeakerService service = new SpeakerService(repository);
            Speaker actualSpeaker = service.GetSpeakerByDisplayName(displayName);

            Assert.That(actualSpeaker, Is.EqualTo(expectedSpeaker));
        }

        [Test]
        public void ShouldGetSpeakerByEmail()
        {
            MockRepository mocks = new MockRepository();
            ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();
            Conference targetConference = new Conference();
            string email = "user@gmail.com";
            string displayName = "Test Speaker";
            Speaker expectedSpeaker =
                new Speaker("Test", "Speaker", "http://www.website.com", "the comment", targetConference,
                             email, displayName, "http://www.website.com/avatar.jpg", "Info about how important I am to go here.", "password", "salt");
            SetupResult.For(repository.GetSpeakerByEmail(email))
                .Return(expectedSpeaker);
            mocks.ReplayAll();

            ISpeakerService service = new SpeakerService(repository);
            Speaker actualSpeaker = service.GetSpeakerByEmail(email);

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

            ISpeakerService service = new SpeakerService(repository);
            IEnumerable<Speaker> speakers = service.GetSpeakers(targetConference, 2, 3);
            List<Speaker> speakersList = new List<Speaker>(speakers);

            Assert.That(speakersList.ToArray(), Is.EqualTo(toReturn));
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
            ISpeakerService service = new SpeakerService(repository);
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

        [Test]
        public void GetCurrentSpeakerProfileShouldUseAuthenticationServiceAndRepository()
        {
            MockRepository mocks = new MockRepository();

            Conference anConference = new Conference();
            Speaker expectedResult =
                new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", anConference,
                             "brownie@brownie.com.au", "AndrewBrowne", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");

            ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();
            Attendee attendee = new Attendee();
            attendee.Contact.Email = "brownie@brownie.com.au";
            IUserSession session = new UserSessionStub(attendee, expectedResult);
            SetupResult.For(repository.GetSpeakerByEmail("brownie@brownie.com.au"))
                .Return(expectedResult);
            mocks.ReplayAll();

            Speaker speaker = session.GetLoggedInSpeaker();

            Assert.AreSame(expectedResult, speaker);
        }

        static Speaker getSpeaker()
        {
            return new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", new Conference(),
                             "brownie@brownie.com.au", "AndrewBrowne", "http://blog.brownie.com.au/avatar.jpg", "Info about how important I am to go here.", "password", "salt");
        }


        [Test]
        public void SaveSpeakerSavesToSpeakerRepository()
        {
            MockRepository mocks = new MockRepository();
            ISpeakerRepository repository = mocks.CreateMock<ISpeakerRepository>();

            Speaker expectedSpeaker = getSpeaker(), actualSpeaker = null;
            Expect.Call(repository.GetSpeakerByEmail("brownie@brownie.com.au"))
                .Return(expectedSpeaker);
            Expect.Call(repository.CanSaveSpeakerWithDisplayName(expectedSpeaker, "UpdatedDisplayName"))
                .Return(true);
            repository.Save(null);
            LastCall.IgnoreArguments()
                .Do(new Action<Speaker>(delegate(Speaker obj) { actualSpeaker = obj; }));
            mocks.ReplayAll();

            ISpeakerService service = new SpeakerService(repository);
            service.SaveSpeaker("brownie@brownie.com.au", "UpdatedFirstName", "UpdatedLastName", "http://updated.website", "UpdatedComment", "UpdatedDisplayName", "updated profile", "http://updated.avatar.url");
            Assert.AreEqual("UpdatedFirstName", actualSpeaker.Contact.FirstName);
            Assert.AreEqual("UpdatedLastName", actualSpeaker.Contact.LastName);
            Assert.AreEqual("http://updated.website", actualSpeaker.Website);
            Assert.AreEqual("UpdatedComment", actualSpeaker.Comment);
            Assert.AreEqual("UpdatedDisplayName", actualSpeaker.DisplayName);
            Assert.AreEqual("updated profile", actualSpeaker.Profile);
            Assert.AreEqual("http://updated.avatar.url", actualSpeaker.AvatarUrl);
        }
    }
}
