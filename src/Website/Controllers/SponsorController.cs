using System;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;
using MvcContrib.Filters;

namespace CodeCampServer.Website.Controllers
{
    public class SponsorController : System.Web.Mvc.Controller
    {
        private readonly IConferenceRepository _conferenceRepository;
        private IAuthorizationService _authorizationService;
        private IClock _clock;

        public SponsorController(IConferenceRepository conferenceRepository, IAuthorizationService authorizationService,
                                 IClock clock)
        {
            _conferenceRepository = conferenceRepository;
            _authorizationService = authorizationService;
            _clock = clock;
        }

        [AdminOnly]
        public ActionResult New(string conferenceKey)
        {
            ViewData.Add(new Sponsor());
            return RenderView("Edit");
        }

        [AdminOnly]
        public ActionResult Delete(string conferenceKey, string sponsorName)
        {            
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var sponsorToDelete = conference.GetSponsor(sponsorName);
            if (sponsorToDelete != null)
            {
                conference.RemoveSponsor(sponsorToDelete);
                _conferenceRepository.Save(conference);
            }
            
            var sponsors = conference.GetSponsors();
            ViewData.Add(sponsors);
            return RenderView("List");
        }


        public ActionResult List(string conferenceKey)
        {
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var sponsors = conference.GetSponsors();
            ViewData.Add(sponsors);
            return RenderView();
        }

        [AdminOnly]
        public ActionResult Edit(string conferenceKey, string sponsorName)
        {
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var sponsor = conference.GetSponsor(sponsorName);
            if (sponsor != null)
            {
                ViewData.Add(sponsor);
                return RenderView();
            }            
            
            return RedirectToAction("List");
        }

        [AdminOnly]
        [PostOnly]
        //TODO: update this to accept a sponsor id to avoid the quirky new/updated logic
        public ActionResult Save(string conferenceKey, string oldName, string name, string level, string logoUrl, string website,
                         string firstName, string lastName, string email)
        {            
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var sponsorLevel = (SponsorLevel) Enum.Parse(typeof (SponsorLevel), level);

            var oldSponsor = conference.GetSponsor(oldName);
            var sponsor = new Sponsor(name, logoUrl, website, firstName, lastName, email, sponsorLevel);
            
            if (oldSponsor != null)
            {
                conference.RemoveSponsor(oldSponsor);
                _conferenceRepository.Save(conference);
            }

            conference.AddSponsor(sponsor);
            _conferenceRepository.Save(conference);

            TempData[TempDataKeys.Message] = "The sponsor was saved.";
            return RedirectToAction("list");
        }
    }
}
