using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class SpeakerControllerTester
    {
        private MockRepository _mocks;        
        private IPersonRepository _personRepository;
        private IConferenceRepository _conferenceRepository;
        private IAuthorizationService _authorizationService;
        private IUserSession _userSession;
        private Conference _conference;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _conferenceRepository = _mocks.CreateMock<IConferenceRepository>();
            _authorizationService = _mocks.CreateMock<IAuthorizationService>();
            _personRepository = _mocks.CreateMock<IPersonRepository>();
            _userSession = _mocks.CreateMock<IUserSession>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
        }
        
        private SpeakerController createSpeakerController()
        {
            return new SpeakerController(_conferenceRepository, _authorizationService, _userSession, new ClockStub());
        }

        [Test]
        public void ShouldViewSpeakerByDisplayName()
        {
            var speaker = getSpeaker();
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);
            _conference.AddSpeaker(speaker.Person, speaker.SpeakerKey, speaker.Bio, speaker.AvatarUrl);                        
            _mocks.ReplayAll();

            var controller = createSpeakerController();
            var result = controller.View("austincodecamp2008", speaker.SpeakerKey) as RenderViewResult;
            var viewDataSpeakerProfile = controller.ViewData.Get<Speaker>();

            Assert.That(viewDataSpeakerProfile, Is.Not.Null);
            Assert.That(viewDataSpeakerProfile, Is.EqualTo(speaker));
            Assert.That(result, Is.Not.Null, "Expected RenderViewResult, but was not");
            Assert.That(result.ViewName, Is.Null, "expected default view");
        }

        [Test]
        public void EditSpeakerShouldGetSpeakerData()
        {
            var speaker = getSpeaker();
            _conference.AddSpeaker(speaker.Person, speaker.SpeakerKey, speaker.Bio, speaker.AvatarUrl);

            SetupResult.For(_userSession.GetLoggedInPerson()).Return(speaker.Person);
            SetupResult.For(_conferenceRepository.GetConferenceByKey(null)).IgnoreArguments().Return(_conference);
            
            _mocks.ReplayAll();

            var controller = createSpeakerController();
            var actionResult = controller.Edit("conf123") as RenderViewResult;

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ViewName, Is.Null, "expected default view");
            
            var viewDataSpeakerProfile = controller.ViewData.Get<Speaker>();
            Assert.That(speaker, Is.EqualTo(viewDataSpeakerProfile));            
        }

        [Test]
        public void EditProfileShouldReturnLoginWhenNoSpeaker()
        {
            SetupResult.For(_conferenceRepository.GetConferenceByKey(null)).IgnoreArguments().Return(_conference);
            SetupResult.For(_userSession.GetLoggedInPerson()).Return(null);
            _mocks.ReplayAll();

            var controller = createSpeakerController();
            var actionResult = controller.Edit("conf123") as ActionRedirectResult;
            
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.Values["controller"].ToString().ToLower(), Is.EqualTo("login"));
            Assert.That(actionResult.Values["action"].ToString().ToLower(), Is.EqualTo("index"));
        }

        //TODO:  rewrite this test
//        [Test, Explicit]
//        public void SaveSpeakerReturnSaveExceptionMessageOnExceptionAndReturnToEditAction()
//        {
//            Speaker savedSpeaker = getSpeaker();
//            
//            _userSession = new UserSessionStub(savedSpeaker.Person);
//
//            string validationMessage = "Validation Error";
//			SetupResult.For(                               
//				.Throw(new DataValidationException(validationMessage));
//            _mocks.ReplayAll();
//
//            var controller = createSpeakerController();
//            controller.Save(_conference.Key, "AndrewBrowne", "Andrew", "Browne", "http://blog.brownie.com.au",
//                            "A comment",
//                            "Info about how important I am to go here.", "http://blog.brownie.com.au/avatar.jpg");
//
//            Assert.That(controller.RedirectToActionValues["Action"], Is.EqualTo("edit"));
//            string viewDataMessage = controller.TempData["error"] as string;
//            Assert.AreEqual(validationMessage, viewDataMessage);
//        }

        [Test]
        public void ShouldListSpeakersForAConference()
        {            
            var p = new Person("joe", "dimaggio", "jd@baseball.com");
            var p2 = new Person("marilyn", "monroe", "m@m.com");

            _conference.AddSpeaker(p, "joedimaggio", "bio here...", "avatar.jpg");
            _conference.AddSpeaker(p2, "marilynmonroe", "bio here...", "avatar.jpg");

            using (_mocks.Record())
            {
                SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
                    .IgnoreArguments()
                    .Return(_conference);
            }

            using(_mocks.Playback())
            {
                var controller = createSpeakerController();
                var actionResult = controller.List("austinCodeCamp2008", 0, 0) as RenderViewResult;

                Assert.That(actionResult, Is.Not.Null);
                Assert.That(actionResult.ViewName, Is.Null, "expected default view");

                var speakersPassedtoView = controller.ViewData.Get<Speaker[]>();
                Assert.That(speakersPassedtoView, Is.Not.Null);
                Assert.That(speakersPassedtoView.Length, Is.EqualTo(2));
            }
        }

        private Speaker getSpeaker()
        {
            var person = new Person("joe", "blow", "jb@aol.com");
            return new Speaker(person, "jb", "bio", "avatar.jpg");
        }
    }
}