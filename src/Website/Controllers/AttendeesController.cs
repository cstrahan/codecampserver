using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using MvcContrib;

namespace CodeCampServer.Website.Controllers
{    
    public class AttendeesController : BaseController
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IClock _clock;

        public AttendeesController(IUserSession userSession, IConferenceRepository conferenceRepository, IClock clock) : 
            base(userSession)
        {
            _conferenceRepository = conferenceRepository;
            _clock = clock;
        }

        public ActionResult List(string conferenceKey)
        {
            var conf = _conferenceRepository.GetConferenceByKey(conferenceKey);            
            var attendees = conf.GetAttendees();

            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var scheduledConference = new Schedule(conference, _clock, null, null);
            
            ViewData.Add(attendees);
            ViewData.Add(scheduledConference);

            return View();
        }

        public ActionResult New(string conferenceKey)
        {            
            ViewData.Add(new Person());

            return View();
        }

    }
}