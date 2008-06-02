using System;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
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
        private IConferenceRepository _conferenceRepository;
        private ITimeSlotRepository _timeSlotRepository;
	    private ITrackRepository _trackRepository;
        private Conference _conference;
        private TimeSlot[] _timeSlots;
        private IViewEngine _viewEngine;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _conferenceRepository = _mocks.CreateMock<IConferenceRepository>();
            _trackRepository = _mocks.CreateMock<ITrackRepository>();
            _timeSlotRepository = _mocks.CreateMock<ITimeSlotRepository>();
            _viewEngine = _mocks.CreateMock<IViewEngine>();
            _conference = new Conference("austincodecamp2008", "Austin Code Camp");
            _timeSlots = new[]
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
            SetupResult.For(_conferenceRepository.GetConferenceByKey("austincodecamp2008")).Return(_conference);
            SetupResult.For(_timeSlotRepository.GetTimeSlotsFor(_conference)).Return(_timeSlots);
        

            ViewContext actualViewContext = null;
            Expect.Call(() => _viewEngine.RenderView(null)).IgnoreArguments()
                .Do(new Action<ViewContext>(
                        context => { actualViewContext = context; }));

            var authorizationService = _mocks.DynamicMock<IAuthorizationService>();
            
            _mocks.ReplayAll();

            var controller = createController(authorizationService);
            var actionResult = controller.Index("austincodecamp2008") as ViewResult;

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ViewName, Is.EqualTo("View"));
            Assert.That(controller.ViewData, Is.Not.Null);
            Assert.That(controller.ViewData.Contains<Schedule>());
            Assert.That(controller.ViewData.Get<Schedule>().Name, Is.EqualTo("Austin Code Camp"));
        }

        private ScheduleController createController(IAuthorizationService authorizationService)
        {
            return new ScheduleController(_conferenceRepository, new ClockStub(),
                                          _timeSlotRepository, _trackRepository,
                                          authorizationService);
        }
    }
}
