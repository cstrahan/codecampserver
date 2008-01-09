using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;

namespace CodeCampServer.Website.Controllers
{
	public class ConferenceController : Controller
	{
		private readonly IConferenceService _conferenceService;
		private IClock _clock;

		public ConferenceController(IConferenceService conferenceService, IClock clock)
		{
			_conferenceService = conferenceService;
			_clock = clock;
		}

		[ControllerAction]
		public void Schedule(string conferenceKey)
		{
			ScheduledConference conference = getScheduledConference(conferenceKey);

			RenderView("showschedule", conference);
		}

		private ScheduledConference getScheduledConference(string conferenceKey)
		{
			Conference conference = _conferenceService.GetConference(conferenceKey);
			return new ScheduledConference(conference, _clock);
		}

		[ControllerAction]
		public void Details(string conferenceKey)
		{
			ScheduledConference conference = getScheduledConference(conferenceKey);
			ViewData.Add("conference", conference);
			RenderView("details");
		}

		[ControllerAction]
		public void PleaseRegister(string conferenceKey)
		{
			ScheduledConference conference = getScheduledConference(conferenceKey);
			ViewData.Add("conference", conference);
			RenderView("registerform");
		}

		[ControllerAction]
		public void Register(string conferenceKey, string firstName, string lastName, string email, string website,
		                     string comment, string password)
		{		    
			ScheduledConference scheduledConference = getScheduledConference(conferenceKey);
			Attendee attendee = _conferenceService.RegisterAttendee(firstName, lastName, website, comment, 
				scheduledConference.Conference, email, password);
			ViewData.Add("attendee", attendee);
			ViewData.Add("conference", scheduledConference);
			RenderView("registerconfirm");
		}

		[ControllerAction]
		public void ListAttendees(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			ScheduledConference scheduledConference = getScheduledConference(conferenceKey);
			IEnumerable<Attendee> attendees = _conferenceService.GetAttendees(scheduledConference.Conference, effectivePage, effectivePerPage);
			IEnumerable<AttendeeListing> listings = getListingsFromAttendees(attendees);
			ViewData.Add("conference", scheduledConference);
			ViewData.Add("attendees", listings);
			RenderView("listattendees");
		}

        [ControllerAction]
        public void New()
        {
            RenderView("New");
        }        

		private IEnumerable<AttendeeListing> getListingsFromAttendees(IEnumerable<Attendee> attendees)
		{
			foreach (Attendee attendee in attendees)
			{
				yield return new AttendeeListing(attendee);
			}
		}
	}
}
