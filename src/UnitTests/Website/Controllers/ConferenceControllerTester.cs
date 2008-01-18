using System;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using CodeCampServer.Website.Models.Conference;
using CodeCampServer.Model.Exceptions;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class ConferenceControllerTester
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

		[Test]
		public void ScheduleShouldGetConferenceByKeyAndSendScheduleToTheView()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			_mocks.ReplayAll();

			TestingConferenceController controller =
				new TestingConferenceController(_service, new ClockStub());
			controller.Schedule("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("showschedule"));
			ScheduledConference actualViewData =
				controller.ActualViewData as ScheduledConference;
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Name, Is.EqualTo("Austin Code Camp"));
		}

		private class TestingConferenceController : ConferenceController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;

            public TestingConferenceController(IConferenceService conferenceService, IClock clock)
				: base(conferenceService, clock)
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
				new TestingConferenceController(_service, new ClockStub());

            controller.Details("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("details"));
			ScheduledConference actualViewData = controller.ActualViewData as ScheduledConference;
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
				new TestingConferenceController(_service, new ClockStub());
			controller.PleaseRegister("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerform"));
			ScheduledConference actualViewData = controller.ViewData["conference"] as ScheduledConference;
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
				new TestingConferenceController(_service, new ClockStub());
			controller.Register("austincodecamp2008", "firstname", "lastname", "email", "website", "comment", "password");

			Assert.That(controller.ActualViewName, Is.EqualTo("registerconfirm"));
			ScheduledConference viewDataConference =
				controller.ViewData["conference"] as ScheduledConference;
			Attendee viewDataAttendee = controller.ViewData["attendee"] as Attendee;
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
				new TestingConferenceController(_service, new ClockStub());
			controller.ListAttendees("austincodecamp2008", 0, 2);

			Assert.That(controller.ActualViewName, Is.EqualTo("listattendees"));
			ListAttendeesViewData viewData = controller.ActualViewData as ListAttendeesViewData;
			            
			Assert.That(viewData, Is.Not.Null);
			Assert.That(viewData.Attendees, Is.Not.Null);
			Assert.That(viewData.Conference, Is.Not.Null);
            Assert.That(viewData.Conference.Conference, Is.EqualTo(_conference));

			List<AttendeeListing> listingList =
				new List<AttendeeListing>(viewData.Attendees);
			Assert.That(listingList.Count, Is.EqualTo(2));
			Assert.That(listingList[0].Name, Is.EqualTo("a b"));
			Assert.That(listingList[1].Name, Is.EqualTo("c d"));
		}

		[Test]
		public void NewActionShouldRenderEditViewWithNewConference()
		{
			TestingConferenceController controller =
				new TestingConferenceController(_service, new ClockStub());
			controller.New();

            Assert.That(controller.ActualViewData, Is.TypeOf(typeof(Conference)));            
			Assert.That(controller.ActualViewName, Is.EqualTo("Edit"));
		}

		
	}
}
