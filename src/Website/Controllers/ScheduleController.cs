using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class ScheduleController : ApplicationController
	{
		private readonly IConferenceService _conferenceService;
		private readonly IClock _clock;
		private readonly ITimeSlotRepository _timeSlotRepository;

		public ScheduleController(IConferenceService conferenceService, IClock clock,
		                          ITimeSlotRepository timeSlotRepository, IAuthorizationService authorizationService)
            :base(authorizationService)
		{
			_conferenceService = conferenceService;
			_clock = clock;
			_timeSlotRepository = timeSlotRepository;
		}

		public void Index(string conferenceKey)
		{
			Conference conference = _conferenceService.GetConference(conferenceKey);
			ScheduledConference scheduledConference = new ScheduledConference(conference, _clock);
		    ScheduleListing[] scheduleListings = getListingsFromTimeSlots(_timeSlotRepository.GetTimeSlotsFor(conference));
		    ViewData.Add(scheduledConference).Add(scheduleListings);
			RenderView("View");
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