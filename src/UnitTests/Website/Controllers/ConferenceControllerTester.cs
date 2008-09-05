using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using MvcContrib;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class ConferenceControllerTester
	{
	    private const string CONFERENCE_KEY = "austinCodeCamp2008";
	    private IConferenceService _service;
		private IUserSession _authSession;
		private Conference _conference;
		private IConferenceRepository _conferenceRepository;

		[SetUp]
		public void Setup()
		{
			_service = MockRepository.GenerateMock<IConferenceService>();
            _authSession = MockRepository.GenerateMock<IUserSession>();
            _conferenceRepository = MockRepository.GenerateMock<IConferenceRepository>();
            _conference = new Conference(CONFERENCE_KEY, "Austin Code Camp") { PubliclyVisible = true };

		    _conferenceRepository.Stub(x => x.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);
		}

		[Test]
		public void index_action_should_get_conference_by_key_and_render_default_view()
		{
		    var controller = createConferenceController();
			controller.Index(CONFERENCE_KEY).ShouldRenderDefaultView();			
            controller.ViewData.Contains<Schedule>().ShouldBeTrue();
			controller.ViewData.Get<Schedule>().Conference.ShouldEqual(_conference);			
		}

		[Test]
		public void list_action_as_admin_should_get_all_conferences_and_render_default_view()
		{
			var conferences = new List<Conference>(new[] {_conference});

		    _conferenceRepository.Expect(x => x.GetAllConferences()).Return(conferences.ToArray());
		    _authSession.Stub(x => x.IsAdministrator).Return(true);
			
			var controller = createConferenceController();		    
            controller.List().ShouldRenderDefaultView();

			var actualConferences = controller.ViewData.Get<Conference[]>();
			Assert.That(actualConferences, Is.Not.Null);
			Assert.That(actualConferences.Length, Is.EqualTo(conferences.Count));
		}      

		[Test]
		public void ShouldGetConferenceForTheRegistrationForm()
		{
			var controller = createConferenceController();
            controller.PleaseRegister(CONFERENCE_KEY).ShouldRenderView("registerform");

            controller.ViewData.Contains<Schedule>().ShouldBeTrue();		    
		    controller.ViewData.Get<Schedule>().Conference.ShouldEqual(_conference);					
		}

		[Test]
		public void ShouldRegisterANewAttendee()
		{
		    var controller = createConferenceController();
			var actualAttendee = new Person();

		    _service.Expect(
		        x => x.RegisterAttendee("firstname", "lastname", "email", "website", "comment", _conference, "password"))
		        .Return(actualAttendee);

		    controller.Register(CONFERENCE_KEY, "firstname", "lastname", "email", "website", "comment", "password")
		        .ShouldRenderView("registerconfirm");

            controller.ViewData.Contains<Schedule>().ShouldBeTrue();
            controller.ViewData.Contains<Person>().ShouldBeTrue();		    			
		}

		[Test]
		public void ListAttendees_should_fetch_attendees_for_conference_and_render_default_view()
		{
			var controller = createConferenceController();
			var person1 = new Person("George", "Carlin", "gcarlin@aol.com");
			var person2 = new Person("Dave", "Chappelle", "rjames@gmail.com");

			_conference.AddAttendee(person1);
			_conference.AddAttendee(person2);
			
			controller.ListAttendees(CONFERENCE_KEY).ShouldRenderDefaultView();
			
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
			var controller = createConferenceController();
			controller.New().ShouldRenderView("edit");
            Assert.That(controller.ViewData.Model, Is.InstanceOfType(typeof(Conference)));
		}
	
		[Test]
		public void SaveCallsConferenceRepositorySave()
		{
			var controller = createConferenceController();
			_conferenceRepository.Stub(x=>x.ConferenceExists("conference", "conf")).Return(false);                        

			controller.Save( new Conference() );

			_conferenceRepository.AssertWasCalled(x => x.Save(null), x => x.IgnoreArguments());
		}

		private ConferenceController createConferenceController()
		{
			return new ConferenceController(_conferenceRepository, _service, _authSession, new ClockStub())
			       	{TempData = new TempDataDictionary()};
		}
	}

	[TestFixture]
	public class when_requesting_conference_details_with_a_private_conference_as_anonymous_user :
		behaves_like_conference_controller_test
	{
		public override void Setup()
		{
			base.Setup();
		    var conference = new Conference("test", "test") {PubliclyVisible = false};
		    _conferenceRepository.Stub(x => x.GetConferenceByKey(null)).IgnoreArguments().Return(conference);
			_userSession.Stub(x => x.IsAdministrator).Return(false);
		}

		[Test]
		public void should_redirect_to_current_conference()
		{
			var result = _conferenceController.Index(null) as RedirectToRouteResult;

			if (result == null)
				Assert.Fail("expected redirect");
			Assert.That(result.Values["action"], Is.EqualTo("current"));
		}
	}

	[TestFixture]
	public class when_requesting_conference_details_with_a_private_conference_as_admin :
		behaves_like_conference_controller_test
	{
		public override void Setup()
		{
			base.Setup();
            
		    var conference = new Conference("test", "test") {PubliclyVisible = false};
		    _conferenceRepository.Stub(x => x.GetConferenceByKey(null)).IgnoreArguments().Return(conference);
		    _userSession.Stub(x => x.IsAdministrator).Return(true);
		}

		[Test]
		public void should_render_details_view()
		{
			_conferenceController.Index(null).ShouldRenderDefaultView();			
		}
	}

	public abstract class behaves_like_conference_controller_test : behaves_like_mock_test
	{
		protected IConferenceService _service;
		protected IUserSession _userSession;
		protected IConferenceRepository _conferenceRepository;
		protected ConferenceController _conferenceController;

		public override void Setup()
		{
			base.Setup();
			_service = Mock<IConferenceService>();
			_userSession = Mock<IUserSession>();
			_conferenceRepository = Mock<IConferenceRepository>();

			_conferenceController = new ConferenceController(_conferenceRepository, _service, _userSession, new ClockStub());
		}
	}
}