using System;
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
        private const string CONFERENCE_KEY = "austinCodeCamp2008";
        private readonly Guid TRACK_GUID = Guid.NewGuid();
        private readonly Guid TIMESLOT_GUID = Guid.NewGuid();
        private IUserSession _authSession;
        private Conference _conference;
        private IConferenceRepository _conferenceRepository;
        private ITrackRepository _trackRepository;
        private ITimeSlotRepository _timeSlotRepository;
        private ISessionRepository _sessionRepository;
        private Track[] _tracks;
        private Track _track;
        private TimeSlot _timeSlot;

        [SetUp]
        public void Setup()
        {
            _conferenceRepository = MockRepository.GenerateMock<IConferenceRepository>();
            _authSession = MockRepository.GenerateMock<IUserSession>();
            _trackRepository = MockRepository.GenerateMock<ITrackRepository>();
            _timeSlotRepository = MockRepository.GenerateMock<ITimeSlotRepository>();
            _sessionRepository = MockRepository.GenerateMock<ISessionRepository>();
            _conference = new Conference(CONFERENCE_KEY, "Austin Code Camp") { PubliclyVisible = true };
            _track = new Track(_conference, "TEST TRACK") { Id = TRACK_GUID };
            _tracks = new[]
                          {
                              _track, 
                              new Track(_conference, "Track1"), 
                              new Track(_conference, "Track2")
                          };

            _timeSlot = new TimeSlot(
                _conference
                , new DateTime(2008, 10, 4, 9, 0, 0)
                , new DateTime(2008, 10, 4, 10, 0, 0)
                , "Session") { Id = TIMESLOT_GUID };
        }

        [Test]
        public void ScheduleShouldGetConferenceByKeyAndSendScheduleToTheView()
        {
            var _timeSlots = new[] {new TimeSlot(), new TimeSlot()};

            _conferenceRepository.Stub(r => r.GetConferenceByKey("austincodecamp2008"))
                .Return(_conference);
            _timeSlotRepository.Stub(r => r.GetTimeSlotsFor(_conference))
                .Return(_timeSlots);
            _trackRepository.Stub(x => x.GetTracksFor(_conference)).Return(_tracks);

            var controller = createScheduleController();

            //Act
            var actionResult = (ViewResult) controller.Index("austincodecamp2008");

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ViewName, Is.EqualTo("View"));
            Assert.That(controller.ViewData.Contains<Schedule>());
            Assert.That(controller.ViewData.Get<Schedule>().Name,
                        Is.EqualTo("Austin Code Camp"));

            Assert.That(controller.ViewData.Contains<Track[]>(), Is.True);
            Assert.That(controller.ViewData.Get<Track[]>(),Is.EqualTo(_tracks));
        }

        [Test]
        public void AdminUserShouldGetEditView()
        {
            _authSession.Stub(x => x.IsAdministrator).Return(true);

            var controller =
                new ScheduleController(_conferenceRepository, new ClockStub(),
                                       _timeSlotRepository, _trackRepository, _sessionRepository, 
                                       _authSession);

            controller.Index("austincodecamp2008").ShouldRenderView("EditView");
        }

        [Test]
        public void edit_action_should_get_track_timeslot_and_sessions_and_render_default_view()
        {
            var controller = createScheduleController();

            Track otherTrack = new Track(_conference, "Other Track");

            var allocatedSession = new Session(_conference, null, string.Empty, string.Empty) { Track = _track };
            _timeSlot.AddSession(allocatedSession);

            var allocatedSessionFromOtherTrack = new Session(_conference, null, string.Empty, string.Empty) { Track = otherTrack };
            _timeSlot.AddSession(allocatedSessionFromOtherTrack);

            var unallocatedSession = new Session(_conference, null, string.Empty, string.Empty) { Track = _track };
            unallocatedSession.IsApproved = true;

            var unallocatedSessionFromOtherTrack = new Session(_conference, null, string.Empty, string.Empty) { Track = otherTrack };
            unallocatedSessionFromOtherTrack.IsApproved = true;

            _conferenceRepository.Stub(x => x.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);
            _sessionRepository.Stub(x => x.GetUnallocatedApprovedSessions(_conference)).Return(new[] { unallocatedSession, unallocatedSessionFromOtherTrack });
            _trackRepository.Stub(x => x.GetTracksFor(_conference)).Return(_tracks);
            _timeSlotRepository.Stub(x => x.GetTimeSlotsFor(_conference)).Return(new[] { _timeSlot });

            controller.Edit(CONFERENCE_KEY, TRACK_GUID, TIMESLOT_GUID).ShouldRenderDefaultView();

            controller.ViewData.Contains<Track>().ShouldBeTrue();
            controller.ViewData.Get<Track>().ShouldEqual(_track);

            controller.ViewData.Contains<TimeSlot>().ShouldBeTrue();
            controller.ViewData.Get<TimeSlot>().ShouldEqual(_timeSlot);

            controller.ViewData.ContainsKey("AllocatedSessions").ShouldBeTrue();
            controller.ViewData.Get<Session[]>("AllocatedSessions").ShouldContain(allocatedSession);
            Assert.AreEqual(1, controller.ViewData.Get<Session[]>("AllocatedSessions").Length);

            controller.ViewData.ContainsKey("UnallocatedSessions").ShouldBeTrue();
            controller.ViewData.Get<Session[]>("UnallocatedSessions").ShouldContain(unallocatedSession);
            Assert.AreEqual(1, controller.ViewData.Get<Session[]>("UnallocatedSessions").Length);
        }

        [Test]
        public void removesession_should_remove_session_and_redirect_to_edit()
        {
            var controller = createScheduleController();

            Guid sessionId = Guid.NewGuid();
            var allocatedSession = new Session(_conference, null, string.Empty, string.Empty) { Track = _track, Id = sessionId };
            _timeSlot.AddSession(allocatedSession);

            _conferenceRepository.Stub(x => x.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);
            _timeSlotRepository.Stub(x => x.GetTimeSlotsFor(_conference)).Return(new[] { _timeSlot });
            _timeSlotRepository.Expect(x => x.Save(_timeSlot));

            controller.RemoveSession(CONFERENCE_KEY, Guid.NewGuid(), TIMESLOT_GUID, sessionId).ShouldRedirectTo("Edit");

            _timeSlotRepository.VerifyAllExpectations();
            Assert.AreEqual(0, _timeSlot.GetSessions().Length);
        }

        [Test]
        public void addsession_should_add_session_and_redirect_to_edit()
        {
            var controller = createScheduleController();

            Guid sessionId = Guid.NewGuid();
            var unallocatedSession = new Session(_conference, null, string.Empty, string.Empty) { Track = _track, Id = sessionId };

            _sessionRepository.Stub(x => x.GetUnallocatedApprovedSessions(_conference)).Return(new[] { unallocatedSession });
            _conferenceRepository.Stub(x => x.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);
            _timeSlotRepository.Stub(x => x.GetTimeSlotsFor(_conference)).Return(new[] { _timeSlot });
            _timeSlotRepository.Expect(x => x.Save(_timeSlot));

            controller.AddSession(CONFERENCE_KEY, Guid.NewGuid(), TIMESLOT_GUID, sessionId).ShouldRedirectTo("Edit");

            _timeSlotRepository.VerifyAllExpectations();
            Assert.AreEqual(1, _timeSlot.GetSessions().Length);
            Assert.AreEqual(unallocatedSession, _timeSlot.GetSessions()[0]);
        }

        [Test]
        public void add_timeslot_should_add_timeslot_and_redirect_to_index()
        {
            var controller = createScheduleController();

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddHours(1);

            _conferenceRepository.Stub(x => x.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);

            _timeSlotRepository.Expect(x => x.Save(new TimeSlot())).Callback(
                new Delegates.Function<bool, TimeSlot>(x => x.StartTime == startTime
                    && x.EndTime == endTime
                    && x.Purpose == "Morning Session 2"
                    && x.Conference == _conference
                    ));

            controller.AddTimeSlot(CONFERENCE_KEY, startTime, endTime, "Morning Session 2").ShouldRedirectTo("Index");

            _timeSlotRepository.VerifyAllExpectations();
        }

        [Test]
        public void add_track_should_add_track_and_redirect_to_index()
        {
            var controller = createScheduleController();

            _conferenceRepository.Stub(x => x.GetConferenceByKey(CONFERENCE_KEY)).Return(_conference);

            _trackRepository.Expect(x => x.Save(new Track())).Callback(
                new Delegates.Function<bool, Track>(x => 
                        x.Name == "New Track"
                        && x.Conference == _conference
                    ));

            controller.AddTrack(CONFERENCE_KEY, "New Track").ShouldRedirectTo("Index");

            _trackRepository.VerifyAllExpectations();
        }

        private ScheduleController createScheduleController()
        {
            return new ScheduleController(_conferenceRepository, new ClockStub(),
                                       _timeSlotRepository, _trackRepository, _sessionRepository, 
                                       _authSession);
        }
    }
}