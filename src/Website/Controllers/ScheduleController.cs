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

		public ScheduleController(IConferenceService conferenceService, IClock clock)
		{
			_conferenceService = conferenceService;
			_clock = clock;
		}

		[ControllerAction]
		public void Index(string conferenceKey)
		{
			Conference conference = _conferenceService.GetConference(conferenceKey);
			ScheduledConference scheduledConference = new ScheduledConference(conference, _clock);
			RenderView("View", new SmartBag(scheduledConference));
		}
	}
}