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

		public ViewResult UpComing(UserGroup group)
		{
			return GetEvents(_eventRepository.GetFutureForUserGroup(group));
		}

		public ViewResult List(UserGroup group)
		{
			return GetEvents(_eventRepository.GetAllForUserGroup(group));
		}

		private ViewResult GetEvents(Event[] events)
		{
			string[] keys = (from e in events select e.Key).ToArray();
			return View("list", keys);
		}
	}
}