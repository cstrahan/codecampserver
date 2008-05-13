using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;
using MvcContrib.Attributes;
using MvcContrib.Filters;

namespace CodeCampServer.Website.Controllers
{
    public class ConferenceController : Controller
    {
        private IClock _clock;
        private readonly IConferenceService _conferenceService;
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceController(IConferenceRepository conferenceRepository,
                                    IConferenceService conferenceService, 
                                    IAuthorizationService authService,
                                    IClock clock) : base(authService)
        {
            _conferenceRepository = conferenceRepository;
            _conferenceService = conferenceService;
            _clock = clock;
        }

        [DefaultAction]
        public ActionResult Details(string conferenceKey)
        {
            Schedule conference = getScheduledConference(conferenceKey);
            ViewData.Add(conference);

            return RenderView();
        }

        public ActionResult Current()
        {
            var conference = _conferenceService.GetCurrentConference();

            //if there are no conferences, then this is likely an admin setting the
            //site up for the first time
            if (conference == null)
                return RedirectToAction("index", "admin");

            return RedirectToAction(
                new RouteValueDictionary(
                    new {controller = "conference", action = "details", conferenceKey = conference.Key}
                    )
                );
        }

        [AdminOnly]
        public ActionResult List()
        {
            var conferences = _conferenceRepository.GetAllConferences();
            ViewData.Add(conferences);
            return RenderView();
        }

        public ActionResult PleaseRegister(string conferenceKey)
        {
            var conference = getScheduledConference(conferenceKey);
            ViewData.Add(conference);
            return RenderView("registerform");
        }

        public ActionResult Register(string conferenceKey, string firstName, string lastName, string email,
                                     string website,
                                     string comment, string password)
        {
            try
            {
                //register the attendee
                var conference = getConferenceByKey(conferenceKey);
                var person = _conferenceService.RegisterAttendee(firstName, lastName, email, website, comment,
                                                                 conference, password);

                //sign them in
                ViewData.Add(person).Add(new Schedule(conference, _clock, null, null));
                return RenderView("registerconfirm");
            }
            catch (Exception exc)
            {
                TempData[TempDataKeys.Error] = "An error occurred while registering your account.";
                Log.Error(this, "An error occcurred while registering a user", exc);
                return RenderView("pleaseregister");
            }
        }

        public ActionResult ListAttendees(string conferenceKey, int? page, int? perPage)
        {
            var effectivePage = page.GetValueOrDefault(0);
            var effectivePerPage = perPage.GetValueOrDefault(20);

            var conference = getConferenceByKey(conferenceKey);
            var attendees = conference.GetAttendees();

            //TODO: implement paging for attendee listing
            //List<Person> pageOfAttendees = new List<Person>(attendees).GetRange(effectivePage * effectivePerPage, effectivePerPage);

            var listings = getListingsFromAttendees(attendees);

            ViewData
                .Add(new Schedule(conference, _clock, null, null))
                .Add(listings);

            return RenderView();
        }

        [AdminOnly]
        public ActionResult New()
        {
            ViewData.Add(new Conference());
            return RenderView("edit");
        }

        [AdminOnly]
        [PostOnly]
        public ActionResult Save(string conf_name, string conf_key, DateTime conf_start, DateTime? conf_end,
                                 string conf_desc)
        {
            if (_conferenceRepository.ConferenceExists(conf_name, conf_key))
            {
                TempData[TempDataKeys.Error] = "A conference has already been created with that name or key";
            }

            var conf = new Conference(conf_key, conf_name) {StartDate = conf_start, EndDate = conf_start};

            try
            {
                _conferenceRepository.Save(conf);
                TempData[TempDataKeys.Message] = "The conference was created successfully.";
                return RedirectToAction("list");
            }
            catch (Exception exc)
            {
                Log.Error("Error saving conference.", exc);
                TempData[TempDataKeys.Error] = "There was an error saving the conference.  The error was: " + exc;
                ViewData.Add("conference", conf);
                return RenderView("edit");
            }
        }

        [AdminOnly]
        public ActionResult Edit(string conferenceKey)
        {
            var conference = getConferenceByKey(conferenceKey);
            if (conference == null)
            {
                TempData[TempDataKeys.Error] = "Conference not found.";
                return RedirectToAction("current", "conference");
            }

            return RenderView("edit");
        }

        private Conference getConferenceByKey(string conferenceKey)
        {
            return _conferenceRepository.GetConferenceByKey(conferenceKey);
        }

        private Schedule getScheduledConference(string conferenceKey)
        {
            var conference = getConferenceByKey(conferenceKey);
            return new Schedule(conference, _clock, null, null);
        }

        private AttendeeListing[] getListingsFromAttendees(Person[] attendees)
        {
            var listings = new List<AttendeeListing>();
            foreach (var attendee in attendees)
            {
                listings.Add(new AttendeeListing(attendee));
            }

            return listings.ToArray();
        }
    }
}