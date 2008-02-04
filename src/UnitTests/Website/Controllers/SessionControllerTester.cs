using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SessionControllerTester
    {
        private MockRepository _mocks;
        private IConferenceService _conferenceService;
        private IAuthorizationService _authorizationService;
        private Conference _conference;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _conferenceService = _mocks.CreateMock<IConferenceService>();
            _authorizationService = _mocks.CreateMock<IAuthorizationService>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
        }

        private class TestingSessionController : SessionController
        {
            public string ActualViewName;
            public string ActualMasterName;
            public object ActualViewData;
            public Hashtable RedirectToActionValues;

            public TestingSessionController(IConferenceService conferenceService, IAuthorizationService authorizationService)
                : base(conferenceService, authorizationService)
            {
            }

            protected override void RenderView(string viewName,
                                               string masterName,
                                               object viewData)
            {
                if (viewData == null)
                    viewData = SmartBag;

                ActualViewName = viewName;
                ActualMasterName = masterName;
                ActualViewData = viewData;
            }

            protected override void RedirectToAction(object values)
            {
                RedirectToActionValues = HtmlExtensionUtility.GetPropertyHash(values);
            }
        }

        [Test]
        public void CreateActionShouldContainSpeakerListingCollectionAndRenderNewView()
        {
            Speaker expectedSpeaker = new Speaker();
            SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_conferenceService.GetLoggedInSpeaker())
                .Return(expectedSpeaker);
            _mocks.ReplayAll();

            TestingSessionController controller = new TestingSessionController(_conferenceService, _authorizationService);
            controller.Create("austincodecamp2008");

            Assert.That(controller.ActualViewName, Is.EqualTo("Create"));
            Assert.That(controller.ActualViewData, Is.SameAs(controller.SmartBag));
            Assert.That(controller.SmartBag.Get<Speaker>(), Is.EqualTo(expectedSpeaker));
        }

        [Test]
        public void CreateActionShouldRedirectToLoginIfUserIsNotASpeaker()
        {
            SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_conferenceService.GetLoggedInSpeaker())
                .Return(null);
            _mocks.ReplayAll();

            TestingSessionController controller = new TestingSessionController(_conferenceService, _authorizationService);
            controller.Create("austincodecamp2008");

            Assert.That(controller.RedirectToActionValues, Is.Not.Null);
            Assert.That(controller.RedirectToActionValues["Controller"], Is.EqualTo("login"));
        }

        [Test]
        public void CreateNewActionShouldCreateNewSession()
        {
            Speaker speaker = new Speaker("first", "last", "http://google.com", "comment", _conference, "email@gmail.com", "display name", "http://avatars.com", "my profile", "password", "salt");
            Session actualSession = new Session(speaker, "title", "abstract");
            actualSession.AddResource(new OnlineResource(OnlineResourceType.Blog, "My Blog", "http://www.myblog.com"));
            actualSession.AddResource(new OnlineResource(OnlineResourceType.Download, "My Download", "http://www.mydownload.com"));
            actualSession.AddResource(new OnlineResource(OnlineResourceType.Website, "My Website", "http://www.mywebsite.com"));
            SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_conferenceService.GetSpeakerByEmail(speaker.Contact.Email))
                .Return(speaker);
            Expect.Call(_conferenceService.CreateSession(actualSession.Speaker, actualSession.Title, actualSession.Abstract, actualSession.GetResources()))
                .IgnoreArguments()
                .Return(actualSession);
            _mocks.ReplayAll();

            TestingSessionController controller = new TestingSessionController(_conferenceService, _authorizationService);
            controller.CreateNew("austincodecamp2008", speaker.Contact.Email, "title", "abstract",
                "My Blog", "http://www.myblog.com", "My Website", "http://www.mywebsite.com", 
                "Session Download", "http://www.mydownload.com");

            Assert.That(controller.ActualViewName, Is.EqualTo("CreateConfirm"));
            Assert.That(controller.ActualViewData, Is.SameAs(controller.SmartBag));
            Session session = controller.SmartBag.Get<Session>();
            Assert.IsNotNull(session);
            Assert.That(session.Speaker.DisplayName, Is.EqualTo(speaker.DisplayName));
            Assert.That(session.Title, Is.EqualTo("title"));
            Assert.That(session.Abstract, Is.EqualTo("abstract"));

            List<OnlineResource> resources = new List<OnlineResource>(session.GetResources());
            Assert.That(resources.Count, Is.EqualTo(3));
            Assert.That(resources.Find(delegate(OnlineResource r) { return r.Name == "My Blog"; }).Href, 
                Is.EqualTo("http://www.myblog.com"));
            Assert.That(resources.Find(delegate(OnlineResource r) { return r.Name == "My Download"; }).Href, 
                Is.EqualTo("http://www.mydownload.com"));
            Assert.That(resources.Find(delegate(OnlineResource r) { return r.Name == "My Website"; }).Href, 
                Is.EqualTo("http://www.mywebsite.com"));
        }

        [Test]
        public void ProposedActionShouldShowProposedSessions()
        {
            IEnumerable<Session> sessions = new List<Session>();
            SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_conferenceService.GetProposedSessions(_conference))
                .Return(sessions);
            _mocks.ReplayAll();

            TestingSessionController controller = new TestingSessionController(_conferenceService, _authorizationService);
            controller.Proposed("austincodecamp2008");

            Assert.That(controller.ActualViewName, Is.EqualTo("Proposed"));
            Assert.That(controller.ActualViewData, Is.SameAs(controller.SmartBag));
            Assert.That(controller.SmartBag.Get<IEnumerable<Session>>(), Is.SameAs(sessions));
        }
    }
}
