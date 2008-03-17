using System;
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

            if(conference == null)
                RedirectToAction("index", "admin");           
            else
                RedirectToAction(new RouteValueDictionary(
                    new { controller="conference", action = "details", conferenceKey = conference.Key })
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
		    SmartBag.Add(conference);
			RenderView("registerform", SmartBag);
		}

		public void Register(string conferenceKey, string firstName, string lastName, string email, string website,
		                     string comment, string password)
		{
            try 
            {
		        Conference conference = _conferenceService.GetConference(conferenceKey);
                Person person = _conferenceService.RegisterAttendee(firstName, lastName, email, website, comment, conference, password);

                //sign in the person

                SmartBag.Add(person).Add(new ScheduledConference(conference, _clock));
                RenderView("registerconfirm", SmartBag);
            } 
            catch(Exception exc)
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

		    SmartBag
		        .Add(new ScheduledConference(conference, _clock))
		        .Add(listings);
            RenderView("listattendees", SmartBag);                                
		}

		public void New()
		{
			RenderView("Edit", new SmartBag().Add(new Conference()));
		}

		private AttendeeListing[] getListingsFromAttendees(Person[] attendees)
		{

			List<AttendeeListing> listings = new List<AttendeeListing>();
			foreach (Person attendee in attendees)
			{
				listings.Add(new AttendeeListing(attendee));
			}

			return listings.ToArray();
		}
	}
}
