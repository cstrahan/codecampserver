using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
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
		private ISessionService _sessionService;
		private IPersonRepository _personRepository;
		private IUserSession _userSession;
		private IAuthorizationService _authorizationService;
		private Conference _conference;
	    private IConferenceRepository _conferenceRepository;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
		    _conferenceRepository = _mocks.CreateMock<IConferenceRepository>();
			_authorizationService = _mocks.CreateMock<IAuthorizationService>();
			_sessionService = _mocks.CreateMock<ISessionService>();
			_personRepository = _mocks.DynamicMock<IPersonRepository>();
			_userSession = _mocks.CreateMock<IUserSession>();
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
		}	

		[Test]
		public void CreateActionShouldContainSpeakerListingCollectionAndRenderNewView()
		{
		    var controller = createController();
		    var person = new Person("Barney", "Rubble", "brubble@aol.com");
		    var expectedSpeaker = new Speaker(person, "brubble", "bio", "avatar");
		    _conference.AddSpeaker(person, expectedSpeaker.SpeakerKey, expectedSpeaker.Bio, expectedSpeaker.AvatarUrl);

		    SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
		    Expect.Call(_userSession.GetLoggedInPerson()).Return(expectedSpeaker.Person);
		    _mocks.ReplayAll();

		    var actionResult = controller.Create("austincodecamp2008") as RenderViewResult;

            Assert.That(actionResult, Is.Not.Null, "expected RenderViewResult");
		    Assert.That(actionResult.ViewName, Is.Null);
			Assert.That(controller.ViewData.Get<Speaker>(), Is.EqualTo(expectedSpeaker));

            _mocks.VerifyAll();		    
		}

	    [Test, Ignore("need to find a good way to test the redirect in this action")]
		public void CreateActionShouldRedirectToLoginIfUserIsNotASpeaker()
		{
	        var controller = createController();
	        SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008"))
				.Return(_conference);
	        Expect.Call(_userSession.GetLoggedInPerson()).Return(null);
	        _mocks.ReplayAll();

	        var actionResult = controller.Create("austincodecamp2008") as ActionRedirectResult;

            Assert.That(actionResult, Is.Not.Null, "should have redirected");
	        Assert.That(actionResult.Values["Controller"], Is.EqualTo("login"));            
		}

	    [Test]
		public void CreateNewActionShouldCreateNewSession()
		{
	        var controller = createController();
	        var speaker = new Person();
	        _conference.AddSpeaker(speaker, "key", "bio", "avatar");

	        var track = new Track("Misc");

	        var actualSession = new Session(_conference, new Person(), "title", "abstract") {Track = track};

	        SetupResult.For(_conferenceRepository.GetConferenceByKey(null)).IgnoreArguments().Return(_conference);
	        SetupResult.For(_personRepository.FindByEmail(null)).IgnoreArguments().Return(speaker);
	        Expect.Call(_sessionService.CreateSession(null, actualSession.Speaker, actualSession.Title, 
                    actualSession.Abstract,actualSession.Track))
				.IgnoreArguments()
				.Return(actualSession);
	        _mocks.ReplayAll();

	        var actionResult =
		        controller.CreateNew("austincodecamp2008", "test@aol.com", "title", "abstract") as RenderViewResult;

            Assert.That(actionResult, Is.Not.Null, "expected RenderViewResult");

		    Assert.That(actionResult.ViewName, Is.EqualTo("CreateConfirm"));
			
            var session = controller.ViewData.Get<Session>();
			Assert.IsNotNull(session);
			
            Assert.That(session.Speaker, Is.EqualTo(speaker));
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
			Expect.Call(_sessionService.GetProposedSessions(_conference))
				.Return(sessions);
			_mocks.ReplayAll();

		    var controller = createController();
		    var actionResult = controller.Proposed("austincodecamp2008") as RenderViewResult;

            Assert.That(actionResult, Is.Not.Null, "expected RenderViewResult");
		    Assert.That(actionResult.ViewName, Is.Null);			
			Assert.That(controller.ViewData.Get<IEnumerable<Session>>(), Is.SameAs(sessions));
		}

	    private SessionController createController()
	    {
	        var fakeHttpContext = _mocks.FakeHttpContext("~/sessions");        
	        var controller = new SessionController(_conferenceRepository, _sessionService, _personRepository,
	                                               _authorizationService, _userSession);
	        controller.ControllerContext = new ControllerContext(fakeHttpContext, new RouteData(), controller);
            return controller;
	    }
	}
}
