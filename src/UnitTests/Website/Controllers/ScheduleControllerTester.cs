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
        private IConferenceService _service;
        private ITimeSlotRepository _timeSlotRepository;
	    private ITrackRepository _trackRepository;
        private Conference _conference;
        private TimeSlot[] _timeSlots;
        private IViewEngine _viewEngine;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            //We use RhinoMocks to create mock objects for our
            //controller dependencies.
            _service = _mocks.CreateMock<IConferenceService>();
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
            //We set up canned results and expectations on our mocks.
            SetupResult.For(_service.GetConference("austincodecamp2008"))
                .Return(_conference);
            SetupResult.For(_timeSlotRepository.GetTimeSlotsFor(_conference))
                .Return(_timeSlots);
            //When the IViewEngine instance is called, we save off the 
            //ViewContext object that is passed along.  We do this so that
            //later we can examine it.
            ViewContext actualViewContext = null;
            Expect.Call(() => _viewEngine.RenderView(null)).IgnoreArguments()
                .Do(new Action<ViewContext>(
                        context => { actualViewContext = context; }));

            var authorizationService = _mocks.DynamicMock<IAuthorizationService>();

            //switching our MockRepository to replay mode.  Now our dynamic
            //mocks are ready to be used.
            _mocks.ReplayAll();

            var controller = new ScheduleController(_service, new ClockStub(),
                                                    _timeSlotRepository, _trackRepository,
                                                    authorizationService);
            controller.ViewEngine = _viewEngine;
            //ControllerContext must be stubbed in the current build.
            controller.ControllerContext = new ControllerContextStub(controller);
            controller.Index("austincodecamp2008");

            //These asserts ensure our controller action did the right thing.
            Assert.That(actualViewContext.ViewName, Is.EqualTo("View"));
            Assert.That(controller.ViewData, Is.Not.Null);
            Assert.That(controller.ViewData.Contains<Schedule>());
            Assert.That(controller.ViewData.Get<Schedule>().Name,
                        Is.EqualTo("Austin Code Camp"));
        }
    }
}
