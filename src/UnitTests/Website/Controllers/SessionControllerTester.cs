using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
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
			_personRepository = _mocks.CreateMock<IPersonRepository>();
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
					viewData = SmartBag;

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
			Speaker expectedSpeaker = new Speaker();
			SetupResult.For(_conferenceService.GetConference("austincodecamp2008")).Return(_conference);
			Expect.Call(_userSession.GetLoggedInPerson()).Return(expectedSpeaker.Person);
			_mocks.ReplayAll();

			TestingSessionController controller =
				new TestingSessionController(_conferenceService, null, null, null, _userSession);
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
			Conference conference = _conferenceService.GetConference("austincodecamp2008");
			Track track = new Track("Misc");
			
            Session actualSession = new Session(new Person(), "title", "abstract");
			
            actualSession.Track = track;
			
            SetupResult.For(conference)
				.Return(_conference);
			
			Expect.Call(
				_sessionService.CreateSession(actualSession.Speaker, actualSession.Title, actualSession.Abstract,
				                              actualSession.Track))
				.IgnoreArguments()
				.Return(actualSession);
			_mocks.ReplayAll();

			TestingSessionController controller =
				new TestingSessionController(_conferenceService, _sessionService, _personRepository, _authorizationService,
				                             _userSession);
		    controller.CreateNew("austincodecamp2008", "test@aol.com", "title", "abstract");

			Assert.That(controller.ActualViewName, Is.EqualTo("CreateConfirm"));
			Assert.That(controller.ActualViewData, Is.SameAs(controller.SmartBag));
			Session session = controller.SmartBag.Get<Session>();
			Assert.IsNotNull(session);
			
            //Assert.That(session.Speaker, Is.EqualTo(speaker.SpeakerKey));
			Assert.That(session.Title, Is.EqualTo("title"));
			Assert.That(session.Abstract, Is.EqualTo("abstract"));			
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
			Assert.That(controller.ActualViewData, Is.SameAs(controller.SmartBag));
			Assert.That(controller.SmartBag.Get<IEnumerable<Session>>(), Is.SameAs(sessions));
		}
	}
}
