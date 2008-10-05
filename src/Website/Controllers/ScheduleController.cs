using System;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using MvcContrib;

namespace CodeCampServer.Website.Controllers
{
	public class ScheduleController : BaseController
	{
		private readonly IConferenceRepository _conferenceRepository;
		private readonly IClock _clock;
		private readonly ITimeSlotRepository _timeSlotRepository;
		private readonly ITrackRepository _trackRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserSession _userSession;

        public ScheduleController(IConferenceRepository conferenceRepository,
                                  IClock clock,
                                  ITimeSlotRepository timeSlotRepository,
                                  ITrackRepository trackRepository,
                                  ISessionRepository sessionRepository,
                                  IUserSession userSession)
            : base(userSession)
        {
            _conferenceRepository = conferenceRepository;
            _clock = clock;
            _timeSlotRepository = timeSlotRepository;
            _trackRepository = trackRepository;
            _sessionRepository = sessionRepository;
            _userSession = userSession;
        }

        public ActionResult Index(string conferenceKey)
        {
            Conference conference =
                _conferenceRepository.GetConferenceByKey(conferenceKey);
            var schedule = new Schedule(conference, _clock,
                                        _timeSlotRepository, _trackRepository);
            ViewData.Add(schedule);

            Track[] tracks = _trackRepository.GetTracksFor(conference);
            ViewData.Add(tracks);
                
            if (_userSession.IsAdministrator)
                return View("EditView");
            
            return View("View");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string conferenceKey, Guid trackId, Guid timeSlotId)
        {
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);

            ViewData.Add(new Schedule(conference, _clock, null, _trackRepository));

            Track[] tracks = _trackRepository.GetTracksFor(conference);
            Track track = tracks.FirstOrDefault(t => t.Id == trackId);
            ViewData.Add(track);

            TimeSlot[] timeSlots = _timeSlotRepository.GetTimeSlotsFor(conference);
            TimeSlot timeSlot = timeSlots.FirstOrDefault(t => t.Id == timeSlotId);
            ViewData.Add(timeSlot);

            ViewData.Add("AllocatedSessions",
                timeSlot.GetSessions().Where(s => s.Track == track).ToArray());

            Session[] unallocated = _sessionRepository.GetUnallocatedApprovedSessions(conference);
            ViewData.Add("UnallocatedSessions",
                unallocated.Where(s => s.Track == track).ToArray());

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveSession(string conferenceKey, Guid trackId, Guid timeSlotId, Guid sessionId)
        {
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);

            TimeSlot[] timeSlots = _timeSlotRepository.GetTimeSlotsFor(conference);
            TimeSlot timeSlot = timeSlots.FirstOrDefault(t => t.Id == timeSlotId);

            Session session = timeSlot.GetSessions().FirstOrDefault(s => s.Id == sessionId);
            timeSlot.RemoveSession(session);

            _timeSlotRepository.Save(timeSlot);

            return RedirectToAction("Edit", new { conferenceKey = conferenceKey, trackId = trackId, timeSlotId = timeSlotId });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddSession(string conferenceKey, Guid trackId, Guid timeSlotId, Guid sessionId)
        {
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);

            TimeSlot[] timeSlots = _timeSlotRepository.GetTimeSlotsFor(conference);
            TimeSlot timeSlot = timeSlots.FirstOrDefault(t => t.Id == timeSlotId);

            Session[] availableSessions = _sessionRepository.GetUnallocatedApprovedSessions(conference);
            Session session = availableSessions.FirstOrDefault(s => s.Id == sessionId);
            timeSlot.AddSession(session);

            _timeSlotRepository.Save(timeSlot);

            return RedirectToAction("Edit", new { conferenceKey = conferenceKey, trackId = trackId, timeSlotId = timeSlotId });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddTimeSlot(string conferenceKey, DateTime startTime, DateTime endTime, string purpose)
	    {
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);

            var timeSlot = new TimeSlot(conference, startTime, endTime, purpose);
            _timeSlotRepository.Save(timeSlot);

	        return RedirectToAction("Index");
	    }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddTrack(string conferenceKey, string description)
	    {
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);

            var track = new Track(conference, description);
            _trackRepository.Save(track);

            return RedirectToAction("Index");
	    }
	}
}
