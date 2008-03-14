using System;
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
	public class ScheduleControllerTester
	{
		private MockRepository _mocks;
		private IConferenceService _service;
		private ITimeSlotRepository _timeSlotRepository;
		private Conference _conference;
		private TimeSlot[] _timeSlots;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_service = _mocks.CreateMock<IConferenceService>();
			_timeSlotRepository = _mocks.CreateMock<ITimeSlotRepository>();
			_conference = new Conference("austincodecamp2008", "Austin Code Camp");
			_timeSlots = new TimeSlot[]
				{
					new TimeSlot(_conference,
					             new DateTime(2008, 1, 1, 8, 0, 0),
					             new DateTime(2008, 1, 1, 9, 0, 0),
					             "Morning Session 1"),
					new TimeSlot(_conference,
					             new DateTime(2008, 1, 1, 10, 0, 0),
					             new DateTime(2008, 1, 1, 11, 0, 0),
					             "Morning Session 2"),
				};
		}

		[Test]
		public void ScheduleShouldGetConferenceByKeyAndSendScheduleToTheView()
		{
			SetupResult.For(_service.GetConference("austincodecamp2008"))
				.Return(_conference);
			SetupResult.For(_timeSlotRepository.GetTimeSlotsFor(_conference))
				.Return(_timeSlots);

		    IAuthorizationService authorizationService = _mocks.DynamicMock<IAuthorizationService>();

			_mocks.ReplayAll();

			TestingScheduleController controller =
				new TestingScheduleController(_service, new ClockStub(), _timeSlotRepository, authorizationService);
			controller.Index("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("View"));
			SmartBag actualViewData =
				controller.ActualViewData as SmartBag;
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Contains<ScheduledConference>());
			Assert.That(actualViewData.Get<ScheduledConference>().Name, Is.EqualTo("Austin Code Camp"));
			Assert.That(actualViewData.Contains<ScheduleListing[]>());
			Assert.That(actualViewData.Get<ScheduleListing[]>().Length, Is.EqualTo(2));
			Assert.That(actualViewData.Get<ScheduleListing[]>()[0].Purpose, Is.EqualTo("Morning Session 1"));
			Assert.That(actualViewData.Get<ScheduleListing[]>()[1].Purpose, Is.EqualTo("Morning Session 2"));
		}

		private class TestingScheduleController : ScheduleController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;

			public TestingScheduleController(IConferenceService conferenceService, IClock clock,
			                                 ITimeSlotRepository timeSlotRepository, IAuthorizationService authorizationService)
                : base(conferenceService, clock, timeSlotRepository, authorizationService)
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
	}
}