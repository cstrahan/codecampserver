using System.Collections.Generic;
using System.Reflection;
using System.Web.Routing;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Exceptions;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using System.Web;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SpeakerControllerTester
    {
        private MockRepository _mocks;
        private IConferenceService _conferenceService;
        private IPersonRepository _personRepository;
        private IAuthorizationService _authorizationService;
        private Conference _conference;
        private IUserSession _userSession;

        private class TestingSpeakerController : SpeakerController
        {
            public string ActualViewName;
            public string ActualMasterName;
            public object ActualViewData;
            public RouteValueDictionary RedirectToActionValues;
            public static TempDataDictionary ActualTempData;

            public void CreateTempData(HttpContextBase context)
            {
                ActualTempData = new TempDataDictionary(context);
                PropertyInfo property = typeof (Controller).GetProperty("TempData");
                property.SetValue(this, ActualTempData, null);
            }

            public TestingSpeakerController(IConferenceService conferenceService, IPersonRepository personRepository,
                                            IAuthorizationService authorizationService, IClock clock,
                                            IUserSession userSession)
                : base(conferenceService, personRepository, authorizationService, clock, userSession)
            {
            }

            protected override void RenderView(string viewName, string masterName, object viewData)
            {
                if (viewData == null)
                    viewData = ViewData;

                ActualViewName = viewName;
                ActualMasterName = masterName;
                ActualViewData = viewData;
            }

            protected override void RedirectToAction(RouteValueDictionary values)
            {
                RedirectToActionValues = values;
            }
        }

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _conferenceService = _mocks.CreateMock<IConferenceService>();
            _authorizationService = _mocks.CreateMock<IAuthorizationService>();
            _personRepository = _mocks.CreateMock<IPersonRepository>();
            _userSession = _mocks.CreateMock<IUserSession>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
        }

        private TestingSpeakerController GetController()
        {
            HttpContextBase context = _mocks.FakeHttpContext("~/speaker");
            TestingSpeakerController controller = new TestingSpeakerController(_conferenceService, _personRepository,
                                                                               _authorizationService, new ClockStub(),
                                                                               _userSession);
            controller.CreateTempData(context);

            return controller;
        }

        [Test]
        public void IndexShouldRedirectToConferenceDetails()
        {
            TestingSpeakerController controller = GetController();
            controller.Index("austincodecamp2008");

            Assert.That(controller.RedirectToActionValues, Is.Not.Null);
            Assert.That(controller.RedirectToActionValues["Controller"], Is.EqualTo("conference"));
            Assert.That(controller.RedirectToActionValues["Action"], Is.EqualTo("details"));
        }

        [Test]
        public void ShouldViewSpeakerByDisplayName()
        {
            Speaker speaker = getSpeaker();
            SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
                .Return(_conference);

            _conference.AddSpeaker(speaker.Person, speaker.SpeakerKey, speaker.Bio, speaker.AvatarUrl);
                        
            _mocks.ReplayAll();

            TestingSpeakerController controller = GetController();
            controller.View("austincodecamp2008", speaker.SpeakerKey);

            Speaker viewDataSpeakerProfile = controller.ViewData.Get<Speaker>();

            Assert.That(viewDataSpeakerProfile, Is.Not.Null);
            Assert.That(viewDataSpeakerProfile, Is.EqualTo(speaker));
            Assert.That(controller.ActualViewName, Is.EqualTo("view"));
        }

        [Test]
        public void EditSpeakerShouldGetSpeakerData()
        {
            Speaker speaker = getSpeaker();
            _conference.AddSpeaker(speaker.Person, speaker.SpeakerKey, speaker.Bio, speaker.AvatarUrl);
            SetupResult.For(_userSession.GetLoggedInPerson()).Return(speaker.Person);
            SetupResult.For(_conferenceService.GetConference(null)).IgnoreArguments().Return(_conference);
            
            _mocks.ReplayAll();

            TestingSpeakerController controller = GetController();
            controller.Edit("conf123");

            Assert.That(controller.ActualViewName, Is.EqualTo("edit"));
            Speaker viewDataSpeakerProfile = controller.ViewData.Get<Speaker>();

            Assert.That(speaker, Is.EqualTo(viewDataSpeakerProfile));
            
        }

        [Test]
        public void EditProfileShouldReturnLoginWhenNoSpeaker()
        {
            SetupResult.For(_conferenceService.GetConference(null)).IgnoreArguments().Return(_conference);
            SetupResult.For(_userSession.GetLoggedInPerson()).Return(null);
            _mocks.ReplayAll();

            TestingSpeakerController controller = GetController();
            controller.Edit("conf123");
            Assert.That(controller.RedirectToActionValues, Is.Not.Null);
            Assert.That(controller.RedirectToActionValues["Controller"], Is.EqualTo("login"));
        }

        [Test, Explicit]
        public void SaveSpeakerReturnSaveExceptionMessageOnExceptionAndReturnToEditAction()
        {
            Speaker savedSpeaker = getSpeaker();
            
            _userSession = new UserSessionStub(savedSpeaker.Person);

            string validationMessage = "Validation Error";
//			SetupResult.For(
//                _speakerService.SaveSpeaker("brownie@brownie.com.au", "Andrew", "Browne", "http://blog.brownie.com.au", "A comment",
//				                     "AndrewBrowne", "Info about how important I am to go here.",
//				                     "http://blog.brownie.com.au/avatar.jpg"))
//				.Throw(new DataValidationException(validationMessage));
            _mocks.ReplayAll();

            TestingSpeakerController controller = GetController();
            controller.Save(_conference.Key, "AndrewBrowne", "Andrew", "Browne", "http://blog.brownie.com.au",
                            "A comment",
                            "Info about how important I am to go here.", "http://blog.brownie.com.au/avatar.jpg");

            Assert.That(controller.RedirectToActionValues["Action"], Is.EqualTo("edit"));
            string viewDataMessage = controller.TempData["error"] as string;
            Assert.AreEqual(validationMessage, viewDataMessage);
        }

        [Test, Explicit]
        public void ShouldListSpeakersForAConference()
        {
            SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
                .Return(_conference);

//			Speaker speaker1 =
//				new Speaker("Andrew", "Browne", "http://blog.brownie.com.au", "the comment", _conference,
//				            "brownie@brownie.com.au", "AndrewBrowne", "http://blog.brownie.com.au/avatar.jpg",
//				            "Info about how important I am to go here.", "password", "salt");
//
//			Speaker speaker2 =
//				new Speaker("Some", "Person", "http://blog.brownie.com.au", "the comment", _conference,
//				            "brownie@brownie.com.au", "AndrewBrowne", "http://blog.brownie.com.au/avatar.jpg",
//				            "Info about how important I am to go here.", "password", "salt");

//			Speaker[] toReturn =
//				new Speaker[] {speaker1, speaker2};
//            SetupResult.For(_speakerService.GetSpeakers(_conference, 0, 2)).Return(toReturn);
//			_mocks.ReplayAll();
//
//			TestingSpeakerController controller = GetController();
//			controller.List("austincodecamp2008", 0, 2);
//
//			Assert.That(controller.ActualViewName, Is.EqualTo("List"));
//            SmartBag viewData = (controller.ActualViewData as SmartBag);
//
//			Assert.That(viewData, Is.Not.Null);
//			Assert.That(viewData.Get<ScheduledConference>(), Is.Not.Null);
//            Assert.That(viewData.Get<SpeakerListingCollection>(), Is.Not.Null);
//            Assert.That(viewData.Get<ScheduledConference>().Conference, Is.EqualTo(_conference));
//
//            List<SpeakerListing> list = new List<SpeakerListing>(viewData.Get<SpeakerListingCollection>());
//			Assert.That(list.Count, Is.EqualTo(2));
//			Assert.That(list[0].Name, Is.EqualTo("Andrew Browne"));
//			Assert.That(list[1].Name, Is.EqualTo("Some Person"));
        }

        private Speaker getSpeaker()
        {
            return new Speaker(new Person("Andrew", "Browne", "brownie@brownie.com.au"), "SavedSpeaker",
                               "Info about how important I am to go here.", "http://blog.brownie.com.au/avatar.jpg");
        }
    }
}