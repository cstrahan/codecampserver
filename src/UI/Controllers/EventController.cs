using System;
using System.Linq;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

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
			EventForm form = null;

			if (@event is Conference)
			{
				form = _conferenceMapper.Map((Conference) @event);
			}
			else
			{
				form = _meetingMapper.Map((Meeting) @event);
			}

			return View(typeName + ANNOUNCEMENT_PARTIAL_SUFFIX, form);
		}

		public ViewResult UpComing(UserGroup userGroup)
		{
			return GetEvents(_eventRepository.GetFutureForUserGroup(userGroup));
		}

		public ViewResult List(UserGroup userGroup)
		{
			return GetEvents(_eventRepository.GetAllForUserGroup(userGroup));
		}

		private ViewResult GetEvents(Event[] events)
		{
			string[] keys = (from e in events select e.Key).ToArray();
			return View("list", keys);
		}

        public ViewResult AllUpcomingEvents()
        {
            var events = _eventRepository.GetAllFutureEvents()
                .Select(currentEvent => new EventList()
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