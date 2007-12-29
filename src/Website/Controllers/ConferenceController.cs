using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;

namespace CodeCampServer.Website.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceController(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }

        [ControllerAction]
        public void Schedule(string conferenceKey)
        {
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            Schedule schedule = new Schedule(conference);

            RenderView("showschedule", schedule);
        }
    }
}