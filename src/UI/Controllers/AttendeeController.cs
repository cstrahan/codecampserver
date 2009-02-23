using System;
using System.Collections.Generic;
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
	[RequiresConferenceFilterAttribute]
	public class AttendeeController : SaveController<Conference, AttendeeForm>
	{
		private readonly IAttendeeMapper _mapper;
		private readonly IConferenceRepository _repository;

		public AttendeeController(IAttendeeMapper mapper, IConferenceRepository repository) : base(repository, mapper)
		{
			_mapper = mapper;
			_repository = repository;
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

		[AutoMappedToModelFilter(typeof(Attendee[]), typeof(AttendeeForm[]))]
		public ViewResult List(Conference conference)
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

			_repository.Save(conference);

			return View();
		}

		[ValidateModel(typeof(AttendeeForm))]
		public ActionResult Save([Bind(Prefix = "")]AttendeeForm form)
		{
			return ProcessSave(form, conference => RedirectToAction<AttendeeController>(c => c.Confirmation()));
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(AttendeeForm form)
		{
			var result = new ValidationResult();
			Conference conference = _repository.GetById(form.ConferenceID);
			if (form.AttendeeID == null && conference.IsAttending(form.EmailAddress))
			{
				result.AddError<AttendeeForm>(c => c.EmailAddress, "Attendee is already registered for this conference.");
			}

			return result.GetAllErrors();
		}

		public ViewResult Confirmation()
		{
			return View();
		}
	}
}