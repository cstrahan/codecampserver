using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IConferenceService _conferenceService;
        private readonly IClock _clock;

        public TrackController(ITrackRepository trackRepository, IConferenceService conferenceService, IClock clock,
                               IAuthorizationService authorizationService)
            : base(authorizationService)
        {
            _trackRepository = trackRepository;
            _conferenceService = conferenceService;
            _clock = clock;
        }

        public ActionResult List(string conferenceKey)
        {
            var conference = _conferenceService.GetConference(conferenceKey);
            var tracks = _trackRepository.GetTracksFor(conference);
            ViewData.Add(new Schedule(conference, _clock, null, _trackRepository));
            ViewData.Add(tracks);

            return RenderView();
        }
    }
}