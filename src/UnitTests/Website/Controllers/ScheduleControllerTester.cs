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
    public class ScheduleControllerTester
    {
        [Test]
        public void ScheduleShouldGetConferenceByKeyAndSendScheduleToTheView()
        {
            //Arrange
            var _conferenceRepository =
                MockRepository.GenerateStub<IConferenceRepository>();
            var _trackRepository =
                MockRepository.GenerateStub<ITrackRepository>();
            var _timeSlotRepository =
                MockRepository.GenerateStub<ITimeSlotRepository>();
            var authorizationService =
                MockRepository.GenerateStub<IUserSession>();

            var _conference =
                new Conference("austincodecamp2008", "Austin Code Camp");
            var _timeSlots = new[] {new TimeSlot(), new TimeSlot()};

            _conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);
            _timeSlotRepository.Stub(r => r.GetTimeSlotsFor(_conference))
                .Return(_timeSlots);

            var controller =
                new ScheduleController(_conferenceRepository, new ClockStub(),
                                       _timeSlotRepository, _trackRepository,
                                       authorizationService);

            //Act
            var actionResult = (ViewResult) controller.Index("austincodecamp2008");

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ViewName, Is.EqualTo("View"));
            Assert.That(controller.ViewData.Contains<Schedule>());
            Assert.That(controller.ViewData.Get<Schedule>().Name,
                        Is.EqualTo("Austin Code Camp"));
        }
    }
}