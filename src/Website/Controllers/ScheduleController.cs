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

        public ScheduleController(IConferenceRepository conferenceRepository,
                                  IClock clock,
                                  ITimeSlotRepository timeSlotRepository,
                                  ITrackRepository trackRepository,
                                  IUserSession userSession)
            : base(userSession)
        {
            _conferenceRepository = conferenceRepository;
            _clock = clock;
            _timeSlotRepository = timeSlotRepository;
            _trackRepository = trackRepository;
        }

        public ActionResult Index(string conferenceKey)
        {
            Conference conference =
                _conferenceRepository.GetConferenceByKey(conferenceKey);
            var schedule = new Schedule(conference, _clock,
                                        _timeSlotRepository, _trackRepository);
            ViewData.Add(schedule);

            return View("View");
        }
    }
}
