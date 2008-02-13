using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
	public class ScheduleController : Controller
	{
		private readonly IConferenceService _conferenceService;
		private readonly IClock _clock;
		private readonly ITimeSlotRepository _timeSlotRepository;

		public ScheduleController(IConferenceService conferenceService, IClock clock,
		                          ITimeSlotRepository timeSlotRepository)
		{
			_conferenceService = conferenceService;
			_clock = clock;
			_timeSlotRepository = timeSlotRepository;
		}

		[ControllerAction]
		public void Index(string conferenceKey)
		{
			Conference conference = _conferenceService.GetConference(conferenceKey);
			ScheduledConference scheduledConference = new ScheduledConference(conference, _clock);
			ScheduleListing[] scheduleListings = getListingsFromTimeSlots(_timeSlotRepository.GetTimeSlotsFor(conference));
			RenderView("View", new SmartBag().Add(scheduledConference).Add(scheduleListings));
		}

		private ScheduleListing[] getListingsFromTimeSlots(IEnumerable<TimeSlot> timeSlots)
		{
			List<ScheduleListing> listings = new List<ScheduleListing>();
			foreach (TimeSlot timeSlot in timeSlots)
			{
				listings.Add(new ScheduleListing(timeSlot));
			}
			return listings.ToArray();
		}
	}
}