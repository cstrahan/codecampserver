using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using CodeCampServer.Model.Security;
using System.Web.Routing;

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
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
		}


	    private class TestingConferenceController : ConferenceController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;
            public RouteValueDictionary ActualRedirectToActionValues;            

            public event EventHandler RedirectedToAction;

			public TestingConferenceController(HttpContextBase context, IConferenceRepository repository, IConferenceService conferenceService, IAuthorizationService authService, IClock clock)
                : base(repository, conferenceService, authService, clock)
			{
                TempData = new TempDataDictionary(context);
			}

			protected override void RenderView(string viewName,
			                                   string masterName,
			                                   object viewData)
			{
				ActualViewName = viewName;
				ActualMasterName = masterName;
				ActualViewData = viewData;
			}

            protected override void RedirectToAction(RouteValueDictionary values)
            {
                ActualRedirectToActionValues = values;
                if (RedirectedToAction != null)
                    RedirectedToAction(this, null);
            }
		}

	    private TestingConferenceController getController()
	    {
	        var fakeContext = _mocks.FakeHttpContext("~/conference");
	        return new TestingConferenceController(fakeContext, _conferenceRepository, _service, _authService, new ClockStub());
	    }

	    [Test]
        public void IndexShouldRedirectToDetailsAction()
        {
	        var controller = getController();           
            controller.Index();

            Assert.AreEqual(controller.ActualRedirectToActionValues["Action"], "details");
        }

	    [Test]
        public void ListAsAdminShouldRenderListViewWithAllConferences()
        {
            List<Conference> conferences = new List<Conference>(new Conference[] { _conference });
            SetupResult.For(_service.GetAllConferences())
                .Return(conferences);
            SetupResult.On(_authService)
                .Call(_authService.IsAdministrator)
                .Return(true);
            _mocks.ReplayAll();

	        var controller = getController();
            controller.List();

            Assert.That(controller.ActualViewName, Is.EqualTo("List"));
            Assert.That(controller.ViewData.Get<IEnumerable<Conference>>(), Is.SameAs(conferences));
        }

	    [Test]
        public void ListAsNonAdminShouldRedirectToLogin()
        {
            SetupResult.On(_authService)
                .Call(_authService.IsAdministrator)
                .Return(false);
            _mocks.ReplayAll();

	        var controller = getController();

            // Have the RedirectToAction event mimic ResposeRedirect by throwing 
            // an exception and causing the end of the request.
            var exception = new ApplicationException("Redirected!");
            controller.RedirectedToAction += delegate { throw exception; };
            try { controller.List(); }
            catch(ApplicationException x) { Assert.That(x, Is.SameAs(exception)); }

            Assert.AreEqual(controller.ActualRedirectToActionValues["Controller"], "Login");
            Assert.AreEqual(controller.ActualRedirectToActionValues["Action"], "index");
        }


	    [Test]
		public void ShouldGetConferenceToShowDetails()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

	        var controller = getController();				

			controller.Details("austincodecamp2008");
			Assert.That(controller.ActualViewName, Is.EqualTo("details"));
			Schedule actualViewData = controller.ViewData.Get<Schedule>();
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Conference, Is.EqualTo(_conference));
		}

	    [Test]
		public void ShouldGetConferenceForTheRegistrationForm()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			var controller = getController();
			controller.PleaseRegister("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerform"));
			Schedule actualViewData = controller.ViewData.Get<Schedule>();
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Conference, Is.EqualTo(_conference));
		}
	 
	    [Test]
		public void ShouldRegisterANewAttendee()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008")).Return(_conference);
			Person actualAttendee = new Person();
			Expect.Call(_service.RegisterAttendee("firstname", "lastname",
			                                      "email", "website", "comment", _conference, "password")).Return(actualAttendee);

			_mocks.ReplayAll();

			TestingConferenceController controller =
                getController();            
			controller.Register("austincodecamp2008", "firstname", "lastname", "email", "website", "comment", "password");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerconfirm"));

			Schedule schedule = controller.ViewData.Get<Schedule>();
			Person viewDataAttendee = controller.ViewData.Get<Person>();

			Assert.That(schedule, Is.Not.Null);
			Assert.That(viewDataAttendee, Is.Not.Null);
			Assert.That(schedule.Conference, Is.EqualTo(_conference));
			Assert.That(viewDataAttendee, Is.EqualTo(actualAttendee));
		}

	    [Test]
		public void ShouldListAttendeesForAConference()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
            _conference.AddAttendee(new Person("George", "Carlin", "gcarlin@aol.com"));
            _conference.AddAttendee(new Person("Dave", "Chappelle", "rjames@gmail.com"));
			
			//SetupResult.For(_service.GetAttendees(_conference, 0, 2)).IgnoreArguments().Return(toReturn);
			_mocks.ReplayAll();

			TestingConferenceController controller = getController();
			controller.ListAttendees("austincodecamp2008", 0, 2);

			Assert.That(controller.ActualViewName, Is.EqualTo("listattendees"));

			AttendeeListing[] attendeeListings = controller.ViewData.Get<AttendeeListing[]>();
			Schedule conference = controller.ViewData.Get<Schedule>();
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
			controller.New();

			Assert.That(controller.ViewData.Contains<Conference>());
			Assert.That(controller.ActualViewName, Is.EqualTo("Edit"));
		}

	    [Test]
        public void SaveActionShouldVerifyUniquenessOfNameAndKey()
        {
            var controller = getController();
	        Expect.Call(_conferenceRepository.ConferenceExists("conference", "conf")).Return(true);

            _mocks.ReplayAll();

            controller.Save("conference", "conf", DateTime.Parse("Dec 12 2007"), null, null);

            _mocks.VerifyAll();
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
	}
}
