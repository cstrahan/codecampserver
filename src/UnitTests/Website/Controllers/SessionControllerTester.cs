using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using System.Web.Routing;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class SessionControllerTester
	{
		private MockRepository _mocks;
		private IConferenceService _conferenceService;
		private ISessionService _sessionService;
		private IPersonRepository _personRepository;
		private IUserSession _userSession;
		private IAuthorizationService _authorizationService;
		private Conference _conference;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_conferenceService = _mocks.CreateMock<IConferenceService>();
			_authorizationService = _mocks.CreateMock<IAuthorizationService>();
			_sessionService = _mocks.CreateMock<ISessionService>();
			_personRepository = _mocks.DynamicMock<IPersonRepository>();
			_userSession = _mocks.CreateMock<IUserSession>();
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
		}

		private class TestingSessionController : SessionController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;
            public RouteValueDictionary RedirectToActionValues;

			public TestingSessionController(IConferenceService conferenceService, ISessionService sessionService,
			                                IPersonRepository personRepository, IAuthorizationService authorizationService,
			                                IUserSession userSession)
				: base(conferenceService, sessionService, personRepository, authorizationService, userSession)
			{
			}

			protected override void RenderView(string viewName,
			                                   string masterName,
			                                   object viewData)
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

		[Test]
		public void CreateActionShouldContainSpeakerListingCollectionAndRenderNewView()
		{
		    var person = new Person("Barney", "Rubble", "brubble@aol.com");
		    Speaker expectedSpeaker = new Speaker(person, "brubble", "bio", "avatar");
		    _conference.AddSpeaker(person, expectedSpeaker.SpeakerKey, expectedSpeaker.Bio, expectedSpeaker.AvatarUrl);

		    SetupResult.For(_userSession.GetLoggedInPerson()).Return(person);		    
			SetupResult.For(_conferenceService.GetConference("austincodecamp2008")).Return(_conference);
			Expect.Call(_userSession.GetLoggedInPerson()).Return(expectedSpeaker.Person);
			_mocks.ReplayAll();

			TestingSessionController controller =
				new TestingSessionController(_conferenceService, null, null, null, _userSession);
			controller.Create("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("Create"));
			Assert.That(controller.ActualViewData, Is.SameAs(controller.ViewData));
			Assert.That(controller.ViewData.Get<Speaker>(), Is.EqualTo(expectedSpeaker));
		}

		[Test]
		public void CreateActionShouldRedirectToLoginIfUserIsNotASpeaker()
		{
			SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
				.Return(_conference);
			Expect.Call(_userSession.GetLoggedInPerson()).Return(null);
			_mocks.ReplayAll();

			TestingSessionController controller =
				new TestingSessionController(_conferenceService, null, null, null, _userSession);
			controller.Create("austincodecamp2008");

			Assert.That(controller.RedirectToActionValues, Is.Not.Null);
			Assert.That(controller.RedirectToActionValues["Controller"], Is.EqualTo("login"));
		}

		[Test]
		public void CreateNewActionShouldCreateNewSession()
		{
            Person speaker = new Person();
            _conference.AddSpeaker(speaker, "key", "bio", "avatar");
			
			Track track = new Track("Misc");
			
            Session actualSession = new Session(_conference, new Person(), "title", "abstract");
			
            actualSession.Track = track;
			
            SetupResult.For(_conferenceService.GetConference(null)).IgnoreArguments().Return(_conference);
		    SetupResult.For(_personRepository.FindByEmail(null)).IgnoreArguments().Return(speaker);
			Expect.Call(
				_sessionService.CreateSession(null, actualSession.Speaker, actualSession.Title, actualSession.Abstract,
				                              actualSession.Track))
				.IgnoreArguments()
				.Return(actualSession);
			_mocks.ReplayAll();

			TestingSessionController controller =
				new TestingSessionController(_conferenceService, _sessionService, _personRepository, _authorizationService,
				                             _userSession);
		    controller.CreateNew("austincodecamp2008", "test@aol.com", "title", "abstract");

			Assert.That(controller.ActualViewName, Is.EqualTo("CreateConfirm"));
			Assert.That(controller.ActualViewData, Is.SameAs(controller.ViewData));
			Session session = controller.ViewData.Get<Session>();
			Assert.IsNotNull(session);
			
            //Assert.That(session.Speaker, Is.EqualTo(speaker.SpeakerKey));
			Assert.That(session.Title, Is.EqualTo("title"));
			Assert.That(session.Abstract, Is.EqualTo("abstract"));			

            _mocks.VerifyAll();
		}

		[Test]
		public void ProposedActionShouldShowProposedSessions()
		{
			IEnumerable<Session> sessions = new List<Session>();
			SetupResult.For(_conferenceService.GetConference("austincodecamp2008"))
				.Return(_conference);
			Expect.Call(_sessionService.GetProposedSessions(_conference))
				.Return(sessions);
			_mocks.ReplayAll();

			TestingSessionController controller =
				new TestingSessionController(_conferenceService, _sessionService, null, null, null);
			controller.Proposed("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("Proposed"));
			Assert.That(controller.ActualViewData, Is.SameAs(controller.ViewData));
			Assert.That(controller.ViewData.Get<IEnumerable<Session>>(), Is.SameAs(sessions));
		}
	}
}
