using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using CodeCampServer.Model.Security;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class ConferenceControllerTester
	{
		private MockRepository _mocks;
		private IConferenceService _service;
	    private IAuthorizationService _authService;
		private Conference _conference;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_service = _mocks.CreateMock<IConferenceService>();
		    _authService = _mocks.DynamicMock<IAuthorizationService>();
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
		}

		private class TestingConferenceController : ConferenceController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;

			public TestingConferenceController(IConferenceService conferenceService, IAuthorizationService authService, IClock clock)
                : base(conferenceService, authService, clock)
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
		public void ShouldGetConferenceToShowDetails()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			TestingConferenceController controller =
				new TestingConferenceController(_service, _authService, new ClockStub());

			controller.Details("austincodecamp2008");
			SmartBag bag = (SmartBag) controller.ActualViewData;

			Assert.That(controller.ActualViewName, Is.EqualTo("details"));
			ScheduledConference actualViewData = bag.Get<ScheduledConference>();
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Conference, Is.EqualTo(_conference));
		}

		[Test]
		public void ShouldGetConferenceForTheRegistrationForm()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
			controller.PleaseRegister("austincodecamp2008");

			SmartBag bag = (SmartBag) controller.ActualViewData;
			Assert.That(controller.ActualViewName, Is.EqualTo("registerform"));
			ScheduledConference actualViewData = bag.Get<ScheduledConference>();
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Conference, Is.EqualTo(_conference));
		}

		[Test]
		public void ShouldRegisterANewAttendee()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008")).Return(_conference);
			Attendee actualAttendee = new Attendee();
			Expect.Call(_service.RegisterAttendee("firstname", "lastname", "website", "comment", _conference,
			                                      "email", "password")).Return(actualAttendee);

			_mocks.ReplayAll();

			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
			controller.Register("austincodecamp2008", "firstname", "lastname", "email", "website", "comment", "password");

			SmartBag bag = (SmartBag) controller.ActualViewData;
			Assert.That(controller.ActualViewName, Is.EqualTo("registerconfirm"));
			ScheduledConference viewDataConference = bag.Get<ScheduledConference>();
			Attendee viewDataAttendee = bag.Get<Attendee>();
			Assert.That(viewDataConference, Is.Not.Null);
			Assert.That(viewDataAttendee, Is.Not.Null);
			Assert.That(viewDataConference.Conference, Is.EqualTo(_conference));
			Assert.That(viewDataAttendee, Is.EqualTo(actualAttendee));
		}

		[Test]
		public void ShouldListAttendeesForAConference()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			Attendee[] toReturn =
				new Attendee[] {new Attendee("a", "b"), new Attendee("c", "d")};
			SetupResult.For(_service.GetAttendees(_conference, 0, 2)).Return(toReturn);
			_mocks.ReplayAll();

			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
			controller.ListAttendees("austincodecamp2008", 0, 2);

			SmartBag bag = (SmartBag) controller.ActualViewData;
			Assert.That(controller.ActualViewName, Is.EqualTo("listattendees"));

			AttendeeListing[] attendeeListings = bag.Get<AttendeeListing[]>();
			ScheduledConference conference = bag.Get<ScheduledConference>();
			Assert.That(attendeeListings, Is.Not.Null);
			Assert.That(conference, Is.Not.Null);
			Assert.That(conference.Conference, Is.EqualTo(_conference));

			Assert.That(attendeeListings.Length, Is.EqualTo(2));
			Assert.That(attendeeListings[0].Name, Is.EqualTo("a b"));
			Assert.That(attendeeListings[1].Name, Is.EqualTo("c d"));
		}

		[Test]
		public void NewActionShouldRenderEditViewWithNewConference()
		{
			TestingConferenceController controller =
                new TestingConferenceController(_service, _authService, new ClockStub());
			controller.New();

			Assert.That(controller.ActualViewData, Is.TypeOf(typeof (SmartBag)));
			SmartBag bag = (SmartBag) controller.ActualViewData;
			Assert.That(bag.Contains<Conference>());
			Assert.That(controller.ActualViewName, Is.EqualTo("Edit"));
		}
	}
}