using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class ScheduleControllerTester
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

			TestingScheduleController controller =
				new TestingScheduleController(_service, new ClockStub());
			controller.Index("austincodecamp2008");

			Assert.That(controller.ActualViewName, Is.EqualTo("View"));
			SmartBag actualViewData =
				controller.ActualViewData as SmartBag;
			Assert.That(actualViewData, Is.Not.Null);
			Assert.That(actualViewData.Contains<ScheduledConference>());
			Assert.That(actualViewData.Get<ScheduledConference>().Name, Is.EqualTo("Austin Code Camp"));
		}

		private class TestingScheduleController : ScheduleController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;

			public TestingScheduleController(IConferenceService conferenceService, IClock clock)
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
	}
}