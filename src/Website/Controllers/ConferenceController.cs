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

		public ConferenceController(IConferenceService conferenceService)
		{
			_conferenceService = conferenceService;
		}

		[ControllerAction]
		public void Schedule(string conferenceKey)
		{
			Conference conference = getConference(conferenceKey);
			Schedule schedule = new Schedule(conference);

			RenderView("showschedule", schedule);
		}

		private Conference getConference(string conferenceKey)
		{
			return _conferenceService.GetConference(conferenceKey);
		}

		[ControllerAction]
		public void Details(string conferenceKey)
		{
			Conference conference = getConference(conferenceKey);
			ViewData.Add("conference", conference);
			RenderView("details");
		}

		[ControllerAction]
		public void PleaseRegister(string conferenceKey)
		{
			Conference conference = getConference(conferenceKey);
			ViewData.Add("conference", conference);
			RenderView("registerform");
		}

		[ControllerAction]
		public void Register(string conferenceKey, string firstName, string lastName, string email, string website,
		                     string comment)
		{
			Conference conference = getConference(conferenceKey);
			Attendee attendee = new Attendee(firstName, lastName, website, comment, conference, email);
			_conferenceService.RegisterAttendee(attendee);
			ViewData.Add("attendee", attendee);
			ViewData.Add("conference", conference);
			RenderView("registerconfirm");
		}

		[ControllerAction]
		public void ListAttendees(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			Conference conference = _conferenceService.GetConference(conferenceKey);
			IEnumerable<Attendee> attendees = _conferenceService.GetAttendees(conference, effectivePage, effectivePerPage);
			IEnumerable<AttendeeListing> listings = getListingsFromAttendees(attendees);
			ViewData.Add("conference", conference);
			ViewData.Add("attendees", listings);
			RenderView("listattendees");
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