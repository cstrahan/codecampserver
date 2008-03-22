using System;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class SponsorController : Controller
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

        public void New(string conferenceKey)
        {
            // TODO IAuthorizationService check
            ViewData.Add(new Sponsor());
            RenderView("Edit");
        }

        public void Delete(string conferenceKey, string sponsorName)
        {
            // TODO IAuthorizationService check
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            Sponsor sponsorToDelete = conference.GetSponsor(sponsorName);
            if (sponsorToDelete != null)
            {
                conference.RemoveSponsor(sponsorToDelete);
                _conferenceRepository.Save(conference);
            }
            Sponsor[] sponsors = conference.GetSponsors();
            ViewData.Add(sponsors);
            RenderView("List");
        }


        public void List(string conferenceKey)
        {
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            Sponsor[] sponsors = conference.GetSponsors();
            ViewData.Add(sponsors);
            RenderView("List");
        }

        public void Edit(string conferenceKey, string sponsorName)
        {
            // TODO IAuthorizationService check
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            Sponsor sponsor = conference.GetSponsor(sponsorName);
            if (sponsor != null)
            {
                ViewData.Add(sponsor);
                RenderView("edit");
            }
            else
            {
                RedirectToAction("List", "Sponsor");
            }
        }

        public void Save(string conferenceKey, string oldName, string name, string level, string logoUrl, string website,
                         string firstName, string lastName, string email)
        {
            // TODO IAuthorizationService check
            Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            SponsorLevel sponsorLevel = (SponsorLevel) Enum.Parse(typeof (SponsorLevel), level);

            Sponsor oldSponsor = conference.GetSponsor(oldName);
            Sponsor sponsor = new Sponsor(name, logoUrl, website, firstName, lastName, email, sponsorLevel);
            
            if (oldSponsor != null)
            {
                conference.RemoveSponsor(oldSponsor);
                _conferenceRepository.Save(conference);
            }
            conference.AddSponsor(sponsor);

            _conferenceRepository.Save(conference);
            
            Sponsor[] sponsors = conference.GetSponsors();
            ViewData.Add(sponsors);
            RenderView("list");
        }
    }
}
