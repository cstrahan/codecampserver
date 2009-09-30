using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class EventController : SmartController
	{
		public const string ANNOUNCEMENT_PARTIAL_SUFFIX = "Announcement";
		private readonly IEventRepository _eventRepository;
		private readonly IConferenceMapper _conferenceMapper;
		private readonly IMeetingMapper _meetingMapper;


		public EventController(IEventRepository eventRepository, IConferenceMapper conferenceMapper,
		                       IMeetingMapper meetingMapper)
		{
			_eventRepository = eventRepository;
			_meetingMapper = meetingMapper;

			_conferenceMapper = conferenceMapper;
		}

		public ViewResult Announcement(Event @event)
		{
			string typeName = @event.GetType().Name;
			EventInput input = null;

			if (@event is Conference)
			{
				input = _conferenceMapper.Map((Conference) @event);
			}
			else
			{
				input = _meetingMapper.Map((Meeting) @event);
			}

			return View(typeName + ANNOUNCEMENT_PARTIAL_SUFFIX, input);
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