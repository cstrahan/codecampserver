using System.Collections.Generic;
using System.Web.Routing;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Exceptions;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class SpeakerController : ApplicationController
    {
        private readonly IConferenceService _conferenceService;
        private readonly IPersonRepository _personRepository;
        private readonly IClock _clock;
        private IUserSession _userSession;

        public SpeakerController(IConferenceService conferenceService,
                                 IPersonRepository personRepository,
                                 IAuthorizationService authorizationService,
                                 IClock clock, IUserSession userSession)
            : base(authorizationService)
        {
            _conferenceService = conferenceService;
            _personRepository = personRepository;
            _clock = clock;
            _userSession = userSession;
        }

        public void Index(string conferenceKey)
        {
            RedirectToAction(new RouteValueDictionary(new
                                                          {
                                                              Controller = "conference",
                                                              Action = "details",
                                                              conferenceKey = conferenceKey
                                                          }));
        }

        public void List(string conferenceKey, int? page, int? perPage)
        {
            int effectivePage = page.GetValueOrDefault(0);
            int effectivePerPage = perPage.GetValueOrDefault(20);

            Conference conference = _conferenceService.GetConference(conferenceKey);
            var scheduledConference = new Schedule(conference, _clock, null);
            IEnumerable<Speaker> speakers = conference.GetSpeakers();
            var speakerListings = new SpeakerListingCollection(speakers);

            ViewData.Add(scheduledConference);
            ViewData.Add(speakerListings);
            ViewData.Add("page", effectivePage);
            ViewData.Add("perPage", effectivePerPage);

            RenderView("List");
        }

        public void View(string conferenceKey, string speakerId)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            Speaker speaker = conference.GetSpeakerByKey(speakerId);
            ViewData.Add(speaker);
            RenderView("view");
        }

        public void Edit(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            Person person = _userSession.GetLoggedInPerson();

            Speaker speaker = null;
            if (person != null)
                speaker = person.GetSpeakerProfileFor(conference);

            if (speaker != null)
            {
                ViewData.Add(speaker);
                RenderView("edit", ViewData);
            }
            else
            {
                RedirectToAction(new RouteValueDictionary(new
                                                              {
                                                                  Controller = "login",
                                                                  Action = "Index"
                                                              }));
            }
        }

        public void Save(string conferenceKey, string displayName, string firstName, string lastName, string website,
                         string comment, string profile, string avatarUrl)
        {
            Person user = _userSession.GetLoggedInPerson();
            try
            {
                //TODO:  replace this with conference.AddSpeaker
                //_speakerService.SaveSpeaker(user.Contact.Email, firstName, lastName, website, comment, displayName, profile, avatarUrl);

                TempData["message"] = "Profile saved";
                RedirectToAction(new RouteValueDictionary(new
                                                              {
                                                                  Action = "view",
                                                                  conferenceKey = conferenceKey,
                                                                  speakerId = displayName
                                                              }));
            }
            catch (DataValidationException ex)
            {
                TempData["error"] = ex.Message;
                RedirectToAction("edit");
            }
        }
    }
}