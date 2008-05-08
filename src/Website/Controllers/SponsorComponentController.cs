using System.Web;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
    public class SponsorComponentController : ComponentControllerBase
    {
        private IConferenceRepository _conferenceRepository;

        public SponsorComponentController(IConferenceRepository _conferenceRepository)
        {
            this._conferenceRepository = _conferenceRepository;
        }

        public void List(string key, SponsorLevel level)
        {
            var conference = _conferenceRepository.GetConferenceByKey(key);
            var sponsors = conference.GetSponsors(level);
            RenderView("List", sponsors);
        }
    }
}