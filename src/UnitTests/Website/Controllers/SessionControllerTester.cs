using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SessionControllerTester
    {
        private MockRepository _mocks;
        private IConferenceService _service;
        private Conference _conference;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _service = _mocks.CreateMock<IConferenceService>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
        }

        private class TestingSessionController : SessionController
        {
            public string ActualViewName;
            public string ActualMasterName;
            public object ActualViewData;

            public TestingSessionController(IConferenceService conferenceService)
                : base(conferenceService)
            {
            }

            protected override void RenderView(string viewName,
                                               string masterName,
                                               object viewData)
            {
                ActualViewName = viewName;
                ActualMasterName = masterName;
                ActualViewData = viewData;
            }

        }

        [Test]
        public void RegisterActionShouldContainSpeakerListingCollectionAndRenderNewView()
        {
            SetupResult.For(_service.GetConference("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_service.GetSpeakers(new Conference(), 0, 0))
                .IgnoreArguments()
                .Return(new List<Speaker> { new Speaker() });
            _mocks.ReplayAll();

            TestingSessionController controller = new TestingSessionController(_service);
            controller.Register("austincodecamp2008");

            Assert.That(controller.ActualViewName, Is.EqualTo("Register"));
            Assert.That((controller.ActualViewData as SmartBag).Get<SpeakerListingCollection>(), Is.Not.Null);
        }

        [Test]
        public void ShouldCreateNewSession()
        {
            Speaker speaker = new Speaker("first", "last", "http://google.com", "comment", _conference, "email@gmail.com", "display name", "http://avatars.com", "my profile", "password", "salt");
            Session actualSession = new Session(speaker, "title", "abstract");
            actualSession.AddResource(new OnlineResource { Name = "My Blog", Type = OnlineResourceType.Blog, Href = "http://www.myblog.com" });
            actualSession.AddResource(new OnlineResource { Name = "My Download", Type = OnlineResourceType.Download, Href = "http://www.mydownload.com" });
            actualSession.AddResource(new OnlineResource { Name = "My Website", Type = OnlineResourceType.Website, Href = "http://www.mywebsite.com" });
            SetupResult.For(_service.GetConference("austincodecamp2008"))
                .Return(_conference);
            Expect.Call(_service.GetSpeakerByDisplayName(speaker.DisplayName))
                .Return(speaker);
            Expect.Call(_service.CreateSession(actualSession.Speaker, actualSession.Title, actualSession.Abstract, actualSession.Resources))
                .IgnoreArguments()
                .Return(actualSession);
            _mocks.ReplayAll();

            TestingSessionController controller = new TestingSessionController(_service);
            controller.Create("austincodecamp2008", speaker.DisplayName, "title", "abstract",
                "My Blog", "http://www.myblog.com", "My Website", "http://www.mywebsite.com", 
                "Session Download", "http://www.mydownload.com");

            Session session = (controller.ActualViewData as SmartBag).Get<Session>();
            Assert.IsNotNull(session);
            Assert.That(session.Speaker.DisplayName, Is.EqualTo(speaker.DisplayName));
            Assert.That(session.Title, Is.EqualTo("title"));
            Assert.That(session.Abstract, Is.EqualTo("abstract"));
            Assert.That(session.Resources.Count, Is.EqualTo(3));
            foreach (OnlineResource resource in session.Resources)
            {
                switch(resource.Type)
                {
                    case OnlineResourceType.Blog:
                        Assert.That(resource.Name, Is.EqualTo("My Blog"));
                        Assert.That(resource.Href, Is.EqualTo("http://www.myblog.com"));
                        break;
                    case OnlineResourceType.Download:
                        Assert.That(resource.Name, Is.EqualTo("My Download"));
                        Assert.That(resource.Href, Is.EqualTo("http://www.mydownload.com"));
                        break;
                    case OnlineResourceType.Website:
                        Assert.That(resource.Name, Is.EqualTo("My Website"));
                        Assert.That(resource.Href, Is.EqualTo("http://www.mywebsite.com"));
                        break;
                    default:
                        Assert.Fail("Unexpected resource found: {0}", resource.Type);
                        break;
                }
            }
        }

    }
}
