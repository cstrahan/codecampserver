using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class ConferenceControllerTester
    {
        private MockRepository _mocks;
        private IConferenceService _service;
        private IAuthorizationService _authService;
        private Conference _conference;
        private IConferenceRepository _conferenceRepository;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _service = _mocks.CreateMock<IConferenceService>();
            _authService = _mocks.DynamicMock<IAuthorizationService>();
            _conferenceRepository = _mocks.DynamicMock<IConferenceRepository>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp") {PubliclyVisible=true};
        }

        [Test]
        public void ShouldGetConferenceToShowDetails()
        {

            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);
            _mocks.ReplayAll();

            var controller = new ConferenceController(_conferenceRepository,
                                                      _service, _authService,
                                                      new ClockStub());

            var actionResult = controller.Details("austincodecamp2008") as RenderViewResult;

            if(actionResult == null)
                Assert.Fail("expected a renderview");

            Assert.That(actionResult.ViewName, Is.Null);

            var schedule = controller.ViewData.Get<Schedule>();
            Assert.That(schedule, Is.Not.Null);
            Assert.That(schedule.Conference, Is.EqualTo(_conference));
        }        

        [Test]
        public void ListAsAdminShouldRenderListViewWithAllConferences()
        {
            var conferences = new List<Conference>(new[] {_conference});
            SetupResult.For(_conferenceRepository.GetAllConferences()).Return(conferences.ToArray());
            SetupResult.For(_authService.IsAdministrator).Return(true);
            _mocks.ReplayAll();

            var controller = getController();
            var actionResult = controller.List() as RenderViewResult;

            if(actionResult == null)
                Assert.Fail("expected RenderViewResult");
            Assert.That(actionResult.ViewName, Is.Null);

            var actualConferences = controller.ViewData.Get<Conference[]>();
            Assert.That(actualConferences, Is.Not.Null);
            Assert.That(actualConferences.Length, Is.EqualTo(conferences.Count));
        }

        [Test]
        public void ShouldGetConferenceForTheRegistrationForm()
        {
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);
            _mocks.ReplayAll();

            var controller = getController();
            var actionResult = controller.PleaseRegister("austincodecamp2008") as RenderViewResult;

            if(actionResult == null) 
                Assert.Fail("expected a renderview");
            Assert.That(actionResult.ViewName, Is.EqualTo("registerform"));

            var actualViewData = controller.ViewData.Get<Schedule>();
            Assert.That(actualViewData, Is.Not.Null);
            Assert.That(actualViewData.Conference, Is.EqualTo(_conference));
        }

        [Test]
        public void ShouldRegisterANewAttendee()
        {
            var controller = getController();
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            var actualAttendee = new Person();

            using (_mocks.Record())
            {
                Expect.Call(
                    _service.RegisterAttendee("firstname", "lastname", "email", "website", "comment", _conference,
                                              "password")
                    ).Return(actualAttendee);
            }

            using (_mocks.Playback())
            {
                var actionResult = controller.Register("austincodecamp2008", "firstname",
                                                       "lastname", "email", "website", "comment", "password") as
                                   RenderViewResult;

                if(actionResult == null)
                    Assert.Fail("expected a renderview");
                Assert.That(actionResult.ViewName, Is.EqualTo("registerconfirm"));

                var schedule = controller.ViewData.Get<Schedule>();
                var viewDataAttendee = controller.ViewData.Get<Person>();

                Assert.That(schedule, Is.Not.Null);
                Assert.That(viewDataAttendee, Is.Not.Null);
                Assert.That(schedule.Conference, Is.EqualTo(_conference));
                Assert.That(viewDataAttendee, Is.EqualTo(actualAttendee));
            }
        }

        [Test]
        public void ListAttendees_should_fetch_attendees_for_conference_and_render_default_view()
        {
            var controller = getController();
            var person1 = new Person("George", "Carlin", "gcarlin@aol.com");
            var person2 = new Person("Dave", "Chappelle", "rjames@gmail.com");

            _conference.AddAttendee(person1);
            _conference.AddAttendee(person2);

            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);

            _mocks.ReplayAll();

            var actionResult = controller.ListAttendees("austincodecamp2008", 0, 2) as RenderViewResult;

            if(actionResult == null)
                Assert.Fail("expected a renderview");
            Assert.That(actionResult.ViewName, Is.Null);

            var attendeeListings = controller.ViewData.Get<AttendeeListing[]>();
            var conference = controller.ViewData.Get<Schedule>();
            Assert.That(attendeeListings, Is.Not.Null);
            Assert.That(conference, Is.Not.Null);
            Assert.That(conference.Conference, Is.EqualTo(_conference));

            Assert.That(attendeeListings.Length, Is.EqualTo(2));
            Assert.That(attendeeListings[0].Name, Is.EqualTo("George Carlin"));
            Assert.That(attendeeListings[1].Name, Is.EqualTo("Dave Chappelle"));
        }

        [Test]
        public void NewActionShouldRenderEditViewWithNewConference()
        {
            var controller = getController();
            var actionResult = controller.New() as RenderViewResult;

            if(actionResult == null)
                Assert.Fail("expected a renderview");
            Assert.That(controller.ViewData.Contains<Conference>());
            Assert.That(actionResult.ViewName.ToLower(), Is.EqualTo("edit"));
        }

        [Test]
        public void SaveActionShouldVerifyUniquenessOfNameAndKey()
        {
            var controller = getController();
            using (_mocks.Record())
            {
                Expect.Call(_conferenceRepository.ConferenceExists("conference", "conf")).Return(true);
            }

            using (_mocks.Playback())
            {
                controller.Save("conference", "conf", DateTime.Parse("Dec 12 2007"), null, null);
            }
        }

        [Test]
        public void SaveWithPreExistingKeySetsErrorMessageToTempData()
        {
            var controller = getController();
            SetupResult.For(_conferenceRepository.ConferenceExists("conference", "conf")).Return(true);

            _mocks.ReplayAll();

            controller.Save("conference", "conf", DateTime.Parse("Dec 12 2007"), null, null);
            Assert.That(controller.TempData.ContainsKey(TempDataKeys.Error));
        }

        [Test]
        public void SaveCallsConferenceRepositorySave()
        {
            var controller = getController();
            SetupResult.For(_conferenceRepository.ConferenceExists("conference", "conf")).Return(false);
            _mocks.ReplayAll();

            controller.Save("conference", "conf", DateTime.Parse("Dec 12 2007"), null, null);

            _mocks.VerifyAll();
        }

        private ConferenceController getController()
        {
            var fakeContext = _mocks.FakeHttpContext("~/conferences");

            return new ConferenceController(_conferenceRepository, _service, _authService, new ClockStub())
                       {TempData = new TempDataDictionary(fakeContext)};
        }
    }

    [TestFixture]
    public class when_requesting_conference_details_with_a_private_conference_as_anonymous_user : behaves_like_conference_controller_test
    {
        public override void Setup()
        {
            base.Setup();
            SetupResult.For(_conferenceRepository.GetConferenceByKey(null)).IgnoreArguments().Return(
                new Conference("test", "test") {PubliclyVisible = false}
                );
            SetupResult.For(_authService.IsAdministrator).Return(false);
            _mocks.ReplayAll();
        }  

        [Test]
        public void should_redirect_to_current_conference()
        {
            var result = _conferenceController.Details(null) as ActionRedirectResult;

            if(result == null)
                Assert.Fail("expected redirect");
            Assert.That(result.Values["action"], Is.EqualTo("current"));
        }
    }

    [TestFixture]
    public class when_requesting_conference_details_with_a_private_conference_as_admin : behaves_like_conference_controller_test
    {
        public override void Setup()
        {
            base.Setup();
            SetupResult.For(_conferenceRepository.GetConferenceByKey(null)).IgnoreArguments().Return(
                new Conference("test", "test") { PubliclyVisible = false }
                );
            SetupResult.For(_authService.IsAdministrator).Return(true);
            _mocks.ReplayAll();
        }

        [Test]
        public void should_render_details_view()
        {
            var result = _conferenceController.Details(null) as RenderViewResult;

            if (result == null)
                Assert.Fail("expected renderview result");
            Assert.That(result.ViewName, Is.Null, "should have rendered default view");
        }
    }

    public abstract class behaves_like_conference_controller_test : behaves_like_mock_test
    {
        protected IConferenceService _service;
        protected IAuthorizationService _authService;        
        protected IConferenceRepository _conferenceRepository;
        protected ConferenceController _conferenceController;

        public override void Setup()
        {
            base.Setup();
            _service = _mocks.DynamicMock<IConferenceService>();
            _authService = _mocks.DynamicMock<IAuthorizationService>();
            _conferenceRepository = _mocks.DynamicMock<IConferenceRepository>();

            _conferenceController = new ConferenceController(_conferenceRepository, _service, _authService, new ClockStub());
        }
    }
}