using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.UI.Services.Impl;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public class AttendeeController : SaveController<Conference, AttendeeForm>
	{
		private readonly IAttendeeUpdater _attendeeUpdater;
		private readonly IConferenceRepository _conferenceRepository;

		public AttendeeController(IAttendeeUpdater attendeeUpdater, IConferenceRepository conferenceRepository)
		{
			_attendeeUpdater = attendeeUpdater;
			_conferenceRepository = conferenceRepository;
		}

		protected override IModelUpdater<Conference, AttendeeForm> GetUpdater()
		{
			return _attendeeUpdater;
		}

		public ViewResult New(Conference conference)
		{
			var model = new AttendeeForm {ConferenceID = conference.Id};
			return View("Edit", model);
		}

		[AutoMappedToModelFilter(typeof (Attendee[]), typeof (AttendeeForm[]))]
		public ViewResult Index(Conference conference)
		{
			Attendee[] attendees = conference.GetAttendees();
			ViewData.Add(attendees);
			return View();
		}

		public ViewResult Confirm(Conference conference, Guid attendeeId)
		{
			Attendee attendee = conference.GetAttendee(attendeeId);
			
			if (attendee == null) return View(ViewNames.Response404);
			
			attendee.Status = AttendanceStatus.Confirmed;
			
			_conferenceRepository.Save(conference);
			
			return View();
		}
	}
}