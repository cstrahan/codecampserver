using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Security;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Website.Controllers
{
    public class TrackController : ApplicationController
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IConferenceService _conferenceService;
        private readonly IClock _clock;

        public TrackController(ITrackRepository trackRepository, IConferenceService conferenceService, IClock clock, IAuthorizationService authorizationService)

            :base(authorizationService)
        {
            _trackRepository = trackRepository;
            _conferenceService = conferenceService;
            _clock = clock;
        }

        [ControllerAction]
        public void List(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            var tracks = _trackRepository.GetTracksFor(conference);
            SmartBag.Add(new ScheduledConference(conference, _clock));
            SmartBag.Add(tracks);

            RenderView("List", SmartBag);
        }
    }
}
