using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class ConferenceController : ApplicationController
	{
	    private readonly IAuthorizationService _authService;
		private readonly IConferenceService _conferenceService;
		private IClock _clock;

		public ConferenceController(IConferenceService conferenceService, IAuthorizationService authService, IClock clock) :base(authService)
		{
			_conferenceService = conferenceService;
		    _authService = authService;
		    _clock = clock;
		}

		private ScheduledConference getScheduledConference(string conferenceKey)
		{
			Conference conference = _conferenceService.GetConference(conferenceKey);
			return new ScheduledConference(conference, _clock);
		}

		public void Index()
		{
			RedirectToAction("details");
		}

		public void Details(string conferenceKey)
		{
			ScheduledConference conference = getScheduledConference(conferenceKey);		    
            SmartBag.Add(conference);
			RenderView("details", SmartBag);
		}

        public void Current()
        {
            Conference conference = _conferenceService.GetCurrentConference();
            RedirectToAction(new RouteValueDictionary(
                new { action = "list", conferenceKey = conference.Key })
            );        
        }

	    public void List()
	    {
	        RequireAdmin();
            IEnumerable<Conference> conferences = _conferenceService.GetAllConferences();
            SmartBag.Add(conferences);
	        RenderView("List", SmartBag);
	    }

        private void RequireAdmin()
        {
            if(! _authService.IsAdministrator)
            {
                RedirectToAction("index", "Login");
            }
        }

		public void PleaseRegister(string conferenceKey)
		{
			ScheduledConference conference = getScheduledConference(conferenceKey);
			RenderView("registerform", new SmartBag().Add(conference));
		}

		public void Register(string conferenceKey, string firstName, string lastName, string email, string website,
		                     string comment, string password)
		{
			ScheduledConference scheduledConference = getScheduledConference(conferenceKey);
			Attendee attendee = _conferenceService.RegisterAttendee(firstName, lastName, website, comment,
			                                                        scheduledConference.Conference, email, password);
			RenderView("registerconfirm", new SmartBag().Add(attendee).Add(scheduledConference));
		}

		public void ListAttendees(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			ScheduledConference scheduledConference = getScheduledConference(conferenceKey);
			Attendee[] attendees =
				_conferenceService.GetAttendees(scheduledConference.Conference, effectivePage, effectivePerPage);
			AttendeeListing[] listings = getListingsFromAttendees(attendees);

			RenderView("listattendees", new SmartBag().Add(scheduledConference).Add(listings));
		}

		public void New()
		{
			RenderView("Edit", new SmartBag().Add(new Conference()));
		}

		private AttendeeListing[] getListingsFromAttendees(IEnumerable<Attendee> attendees)
		{
			List<AttendeeListing> listings = new List<AttendeeListing>();
			foreach (Attendee attendee in attendees)
			{
				listings.Add(new AttendeeListing(attendee));
			}

			return listings.ToArray();
		}
	}
}
