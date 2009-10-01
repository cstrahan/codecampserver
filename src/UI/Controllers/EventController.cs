using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
		private readonly IMappingEngine _meetingMapper;


		public EventController(IEventRepository eventRepository, IConferenceMapper conferenceMapper,
		                       IMappingEngine meetingMapper)
		{
			_eventRepository = eventRepository;
			_meetingMapper = meetingMapper;

			_conferenceMapper = conferenceMapper;
		}

		public ViewResult Announcement(Event @event)
		{
			string typeName = @event.GetType().Name;
			object announcementDisplay = null;

			if (@event is Conference)
			{
				announcementDisplay = _conferenceMapper.Map((Conference) @event);
			}
			else
			{
				announcementDisplay = 
					_meetingMapper.Map<Meeting, MeetingAnnouncementDisplay>((Meeting) @event);
			}

			return View(typeName + ANNOUNCEMENT_PARTIAL_SUFFIX, announcementDisplay);
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