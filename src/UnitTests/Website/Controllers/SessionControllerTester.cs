using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
        private MockRepository _mocks;
        private ISessionRepository _sessionRepository;
        private IPersonRepository _personRepository;
        private IUserSession _userSession;
        private Conference _conference;
        private IConferenceRepository _conferenceRepository;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _conferenceRepository = _mocks.CreateMock<IConferenceRepository>();
            _sessionRepository = _mocks.CreateMock<ISessionRepository>();
            _personRepository = _mocks.DynamicMock<IPersonRepository>();
            _userSession = _mocks.CreateMock<IUserSession>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
        }

        [Test]
        public void CreateActionShouldContainSpeakerListingCollectionAndRenderNewView()
        {
            SessionController controller = createController();
            var person = new Person("Barney", "Rubble", "brubble@aol.com");
            var expectedSpeaker = new Speaker(person, "brubble", "bio", "avatar");
            _conference.AddSpeaker(person, expectedSpeaker.SpeakerKey, expectedSpeaker.Bio, expectedSpeaker.AvatarUrl);

            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            Expect.Call(_userSession.GetLoggedInPerson()).Return(expectedSpeaker.Person);
            _mocks.ReplayAll();

            var actionResult = controller.Create("austincodecamp2008") as ViewResult;

            if (actionResult == null)
                Assert.Fail("expected ViewResult");
            Assert.That(actionResult.ViewName, Is.Null);
            Assert.That(controller.ViewData.Get<Speaker>(), Is.EqualTo(expectedSpeaker));

            _mocks.VerifyAll();
        }

        [Test]
        public void CreateActionShouldRedirectToLoginIfUserIsNotASpeaker()
        {
            SessionController controller = createController();
            controller.ControllerContext = new ControllerContextStub(controller,
                                                                     new HttpContextStub(
                                                                         new HttpRequestStub(
                                                                             new Uri("http://x/path?query=x"))));
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_userSession.GetLoggedInPerson()).Return(null);
            _mocks.ReplayAll();

            var actionResult = (RedirectToRouteResult) controller.Create("austincodecamp2008");

            Assert.That(actionResult.Values["Controller"], Is.EqualTo("login"));
            Assert.That(actionResult.Values["redirecturl"], Is.EqualTo("/path?query=x"));
        }

        [Test]
        public void CreateNewActionShouldCreateNewSession()
        {
            SessionController controller = createController();
            var personSpeaking = new Person("Jeffrey", "Palermo", "e@mail.com");
            _conference.AddSpeaker(personSpeaking, "key", "bio", "avatar");

            var track = new Track("Misc");

            Session actualSession = null;

            SetupResult.For(_conferenceRepository.GetConferenceByKey(null)).IgnoreArguments().Return(_conference);
            SetupResult.For(_personRepository.FindByEmail(null)).IgnoreArguments().Return(personSpeaking);
            _sessionRepository.Save(null);
            LastCall.IgnoreArguments().Do(new Action<Session>(x => actualSession = x));
            _mocks.ReplayAll();

            var actionResult =
                controller.CreateNew("austincodecamp2008", "test@aol.com", "title", "abstract") as ViewResult;

            if (actionResult == null)
                Assert.Fail("expected ViewResult");

            Assert.That(actionResult.ViewName, Is.EqualTo("CreateConfirm"));

            var session = controller.ViewData.Get<Session>();
            Assert.IsNotNull(session);
            Assert.That(session, Is.EqualTo(actualSession));
            Assert.That(session.Speaker, Is.EqualTo(personSpeaking));
            Assert.That(session.Title, Is.EqualTo("title"));
            Assert.That(session.Abstract, Is.EqualTo("abstract"));

            _mocks.VerifyAll();
        }

        [Test]
        public void ProposedActionShouldShowProposedSessions()
        {
            IEnumerable<Session> sessions = new List<Session>();
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_sessionRepository.GetProposedSessions(_conference))
                .Return(sessions);
            _mocks.ReplayAll();

            SessionController controller = createController();
            var actionResult = controller.Proposed("austincodecamp2008") as ViewResult;

            if (actionResult == null)
                Assert.Fail("expected ViewResult");

            Assert.That(actionResult.ViewName, Is.Null);
            Assert.That(controller.ViewData.Get<IEnumerable<Session>>(), Is.SameAs(sessions));
        }

        private SessionController createController()
        {
            HttpContextBase fakeHttpContext = _mocks.FakeHttpContext("~/sessions");
            var controller = new SessionController(_conferenceRepository, _sessionRepository, _personRepository,
                                                   _userSession);
            controller.ControllerContext = new ControllerContext(fakeHttpContext, new RouteData(), controller);
            return controller;
        }
    }
}