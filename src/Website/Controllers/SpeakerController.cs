using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Exceptions;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;
using MvcContrib.Attributes;
using MvcContrib.Filters;

namespace CodeCampServer.Website.Controllers
{
    public class SpeakerController : Controller
    {
        private readonly IClock _clock;
        private readonly IUserSession _userSession;
        private readonly IConferenceRepository _conferenceRepository;

        public SpeakerController(IConferenceRepository conferenceRepository, IAuthorizationService authorizationService, IUserSession userSession, IClock clock)
            : base(authorizationService)
        {            
            _conferenceRepository = conferenceRepository;
            _clock = clock;
            _userSession = userSession;
        }

        [DefaultAction]
        public ActionResult List(string conferenceKey, int? page, int? perPage)
        {
            var effectivePage = page.GetValueOrDefault(0);
            var effectivePerPage = perPage.GetValueOrDefault(20);

            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var scheduledConference = new Schedule(conference, _clock, null, null);
            var speakers = conference.GetSpeakers();            

            ViewData.Add(scheduledConference);
            ViewData.Add(speakers);
            ViewData.Add("page", effectivePage);
            ViewData.Add("perPage", effectivePerPage);

            return View();
        }

        public ActionResult Details(string conferenceKey, string speakerId)
        {
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var speaker = conference.GetSpeakerByKey(speakerId);
            ViewData.Add(speaker);
            return View();
        }

        [RequireLogin]
        public ActionResult Edit(string conferenceKey)
        {
            var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
            var person = _userSession.GetLoggedInPerson();

            Speaker speaker = null;
            if (person != null)
                speaker = person.GetSpeakerProfileFor(conference);

            if (speaker != null)
            {
                ViewData.Add(speaker);
                return View();
            }

            return RedirectToAction("index", "login");
        }
        
        [RequireLogin]
        [PostOnly]
        public ActionResult Save(string conferenceKey, string speakerKey, string bio, string avatarUrl)
        {
            var person = _userSession.GetLoggedInPerson();
            try
            {
                var conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
                conference.AddSpeaker(person, speakerKey, bio, avatarUrl);                

                TempData[TempDataKeys.Message] = "Profile saved";
                return RedirectToAction("view", new { conferenceKey=conferenceKey, speakerId = speakerKey });
            }
            catch (DataValidationException ex)
            {
                TempData[TempDataKeys.Error] = "Error saving your speaker record.  The error has been logged.";
                Log.Error(this, "Error saving speaker.", ex);             
                return RedirectToAction("edit");
            }
        }
    }
}