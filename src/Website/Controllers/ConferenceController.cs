using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;

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
			SmartBag bag = new SmartBag(conference);
			RenderView("showschedule", bag);
		}

		private ScheduledConference getScheduledConference(string conferenceKey)
		{
			Conference conference = _conferenceService.GetConference(conferenceKey);
			return new ScheduledConference(conference, _clock);
		}

		[ControllerAction]
		public void Index()
		{
			RedirectToAction("details");
		}

		[ControllerAction]
		public void Details(string conferenceKey)
		{
			ScheduledConference conference = getScheduledConference(conferenceKey);
			RenderView("details", new SmartBag(conference));
		}

		[ControllerAction]
		public void PleaseRegister(string conferenceKey)
		{
			ScheduledConference conference = getScheduledConference(conferenceKey);
			RenderView("registerform", new SmartBag(conference));
		}

		[ControllerAction]
		public void Register(string conferenceKey, string firstName, string lastName, string email, string website,
		                     string comment, string password)
		{
			ScheduledConference scheduledConference = getScheduledConference(conferenceKey);
			Attendee attendee = _conferenceService.RegisterAttendee(firstName, lastName, website, comment,
			                                                        scheduledConference.Conference, email, password);
			RenderView("registerconfirm", new SmartBag(attendee, scheduledConference));
		}

		[ControllerAction]
		public void ListAttendees(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			ScheduledConference scheduledConference = getScheduledConference(conferenceKey);
			Attendee[] attendees =
				_conferenceService.GetAttendees(scheduledConference.Conference, effectivePage, effectivePerPage);
			AttendeeListing[] listings = getListingsFromAttendees(attendees);

			RenderView("listattendees", new SmartBag(scheduledConference, listings));
		}

		[ControllerAction]
		public void New()
		{
			RenderView("Edit", new SmartBag(new Conference()));
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