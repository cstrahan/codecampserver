using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class SponsorController : Controller
    {
        private readonly IConferenceService _conferenceService;
        private IClock _clock;
        
    	public SponsorController(IConferenceService conferenceService, IClock clock)
		{
            _conferenceService = conferenceService;
			_clock = clock;
		}

        [ControllerAction]
        public void List(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            ConfirmedSponsor[] sponsors = conference.GetSponsors();
            SmartBag viewData = new SmartBag().Add(sponsors);
            RenderView("List", viewData);    
        }
    }
}
