using System;
using System.Collections.Generic;
using System.Web.Routing;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly IAuthorizationService _authService;
        private readonly IConferenceService _conferenceService;
        private IClock _clock;

        public ConferenceController(IConferenceService conferenceService, IAuthorizationService authService,
                                    IClock clock) : base(authService)
        {
            _conferenceService = conferenceService;
            _authService = authService;
            _clock = clock;
        }

        private Schedule getScheduledConference(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            return new Schedule(conference, _clock, null);
        }

        public void Index()
        {
            RedirectToAction("details");
        }

        public void Details(string conferenceKey)
        {
            Schedule conference = getScheduledConference(conferenceKey);
            ViewData.Add(conference);
            RenderView("details", ViewData);
        }

        public void Current()
        {
            Conference conference = _conferenceService.GetCurrentConference();

            if (conference == null)
                RedirectToAction("index", "admin");
            else
                RedirectToAction(new RouteValueDictionary(
                                     new {controller = "conference", action = "details", conferenceKey = conference.Key})
                    );
        }

        public void List()
        {
            RequireAdmin();
            IEnumerable<Conference> conferences = _conferenceService.GetAllConferences();
            ViewData.Add(conferences);
            RenderView("List", ViewData);
        }

        private void RequireAdmin()
        {
            if (! _authService.IsAdministrator)
            {
                RedirectToAction("index", "Login");
            }
        }

        public void PleaseRegister(string conferenceKey)
        {
            Schedule conference = getScheduledConference(conferenceKey);
            ViewData.Add(conference);
            RenderView("registerform", ViewData);
        }

        public void Register(string conferenceKey, string firstName, string lastName, string email, string website,
                             string comment, string password)
        {
            try
            {
                Conference conference = _conferenceService.GetConference(conferenceKey);
                Person person = _conferenceService.RegisterAttendee(firstName, lastName, email, website, comment,
                                                                    conference, password);

                //sign in the person

                ViewData.Add(person).Add(new Schedule(conference, _clock, null));
                RenderView("registerconfirm", ViewData);
            }
            catch (Exception exc)
            {
                TempData["error"] = "An error occurred while registering your account.";
                Log.Error(this, "An error occcurred while registering a user", exc);
                RenderView("pleaseregister");
            }
        }

        public void ListAttendees(string conferenceKey, int? page, int? perPage)
        {
            int effectivePage = page.GetValueOrDefault(0);
            int effectivePerPage = perPage.GetValueOrDefault(20);

            Conference conference = _conferenceService.GetConference(conferenceKey);
            Person[] attendees = conference.GetAttendees();

            //TODO: implement paging for attendee listing
            //List<Person> pageOfAttendees = new List<Person>(attendees).GetRange(effectivePage * effectivePerPage, effectivePerPage);

            AttendeeListing[] listings = getListingsFromAttendees(attendees);

            ViewData
                .Add(new Schedule(conference, _clock, null))
                .Add(listings);
            RenderView("listattendees", ViewData);
        }

        public void New()
        {
            ViewData.Add(new Conference());
            RenderView("Edit");
        }

        private AttendeeListing[] getListingsFromAttendees(Person[] attendees)
        {
            var listings = new List<AttendeeListing>();
            foreach (Person attendee in attendees)
            {
                listings.Add(new AttendeeListing(attendee));
            }

            return listings.ToArray();
        }
    }
}