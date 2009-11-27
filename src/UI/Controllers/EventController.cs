using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.ActionResults;
//using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class EventController : ConventionController
	{
		public const string ANNOUNCEMENT_PARTIAL_SUFFIX = "Announcement";
		private readonly IEventRepository _eventRepository;


		public EventController(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public ViewResult Announcement(Event @event)
		{
			var result = AutoMappedView<ConferenceInput>(@event);
			result.ViewName = @event.GetType().Name + ANNOUNCEMENT_PARTIAL_SUFFIX;

			if (@event is Meeting)
			{
				result.ViewModelType = typeof (MeetingAnnouncementDisplay);
			}

			return result;
		}

		public ViewResult UpComing(UserGroup userGroup)
		{
			Event[] events = _eventRepository.GetFutureForUserGroup(userGroup);
			return GetEvents(events);
		}

		public ViewResult List(UserGroup userGroup)
		{
			Event[] events = _eventRepository.GetAllForUserGroup(userGroup);
			return GetEvents(events);
		}

		private ViewResult GetEvents(IEnumerable<Event> events)
		{
			string[] keys = (from e in events select e.Key).ToArray();
			return View("list", keys);
		}

		public ViewResult AllUpcomingEvents()
		{
			EventList[] events = _eventRepository.GetAllFutureEvents()
				.Select(currentEvent => new EventList
				                        	{
				                        		Date = currentEvent.Date(),
				                        		Title = currentEvent.Title(),
				                        		UserGroupName = currentEvent.UserGroup.Name,
				                        		UserGroupDomainName = currentEvent.UserGroup.DomainName
				                        	}).ToArray();
			return View(events);
		}
	}
}