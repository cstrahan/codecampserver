using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Controllers;
using MvcContrib;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SessionControllerTester
    {
        private const string CONFERENCE_KEY = "austincodecamp2008";
        private ISessionRepository _sessionRepository;
        private IPersonRepository _personRepository;
        private IUserSession _userSession;
        private Conference _conference;
        private IConferenceRepository _conferenceRepository;

        [SetUp]
        public void Setup()
        {
            _conferenceRepository = MockRepository.GenerateMock<IConferenceRepository>();
            _sessionRepository = MockRepository.GenerateMock<ISessionRepository>();
            _personRepository = MockRepository.GenerateMock<IPersonRepository>();
            _userSession = MockRepository.GenerateMock<IUserSession>();
            _conference = new Conference(CONFERENCE_KEY, "Austin Code Camp");

            _conferenceRepository.Stub(x => x.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);
        }

        [Test]
        public void CreateActionShouldContainSpeakerListingCollectionAndRenderNewView()
        {
            var controller = createSessionController();
            var person = new Person("Barney", "Rubble", "brubble@aol.com");
            var expectedSpeaker = new Speaker(person, "brubble", "bio", "avatar");
            _conference.AddSpeaker(person, expectedSpeaker.SpeakerKey, expectedSpeaker.Bio, expectedSpeaker.AvatarUrl);
            
            _userSession.Expect(x=>x.GetLoggedInPerson()).Return(expectedSpeaker.Person);            

            controller.Create(CONFERENCE_KEY).ShouldRenderDefaultView();

            Assert.That(controller.ViewData.Get<Speaker>(), Is.EqualTo(expectedSpeaker));

            _userSession.VerifyAllExpectations();
        }

        [Test]
        public void CreateActionShouldRedirectToLoginIfUserIsNotASpeaker()
        {            
            var controller = createSessionController();
            controller.ControllerContext = new ControllerContextStub(controller,
                                                                     new HttpContextStub(
                                                                         new HttpRequestStub(
                                                                             new Uri("http://x/path?query=x"))));                        
            _userSession.Expect(x=>x.GetLoggedInPerson()).Return(null);

            controller.Create(CONFERENCE_KEY).ShouldRedirectTo("login", "index")
                .WithValue("redirectUrl", "/path?query=x");
        }

        [Test]
        public void CreateNewActionShouldCreateNewSession()
        {
            var controller = createSessionController();
            var personSpeaking = new Person("Jeffrey", "Palermo", "e@mail.com");
            _conference.AddSpeaker(personSpeaking, "key", "bio", "avatar");

            Session actualSession = null;

            _conferenceRepository.Stub(x=>x.GetConferenceByKey(null)).IgnoreArguments().Return(_conference);
            _personRepository.Stub(x=>x.FindByEmail(null)).IgnoreArguments().Return(personSpeaking);
            _sessionRepository.Expect(x => x.Save(null)).IgnoreArguments()
                .Do(new Action<Session>(x=> actualSession = x));
            
            controller.CreateNew(CONFERENCE_KEY, "test@aol.com", "title", "abstract").ShouldRenderView("CreateConfirm");

            controller.ViewData.Contains<Session>().ShouldBeTrue();

            var session = controller.ViewData.Get<Session>();
            Assert.That(session, Is.EqualTo(actualSession));
            Assert.That(session.Speaker, Is.EqualTo(personSpeaking));
            Assert.That(session.Title, Is.EqualTo("title"));
            Assert.That(session.Abstract, Is.EqualTo("abstract"));

            _sessionRepository.VerifyAllExpectations();            
        }

        [Test]
        public void ProposedActionShouldShowProposedSessions()
        {
            var sessions = new List<Session>();
            _conferenceRepository.Stub(x=>x.GetConferenceByKey(CONFERENCE_KEY))
                .Return(_conference);
            _sessionRepository.Expect(x=>x.GetProposedSessions(_conference))
                .Return(sessions);

            var controller = createSessionController();
            controller.Proposed(CONFERENCE_KEY).ShouldRenderDefaultView();
            
            Assert.That(controller.ViewData.Get<IEnumerable<Session>>(), Is.SameAs(sessions));
        }

        private SessionController createSessionController()
        {            
            var controller = new SessionController(_conferenceRepository, _sessionRepository, _personRepository, _userSession);
            return controller;
        }
    }
}