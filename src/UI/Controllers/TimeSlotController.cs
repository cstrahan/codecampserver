using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilterAttribute]
	public class TimeSlotController : SaveController<TimeSlot, TimeSlotForm>
	{
		private readonly ITimeSlotRepository _repository;
		private readonly ITimeSlotMapper _mapper;
		private readonly ISessionRepository _sessionsRepository;
	    private readonly ISecurityContext _securityContext;

	    public TimeSlotController(ITimeSlotRepository repository, ITimeSlotMapper mapper, ISessionRepository sessionsRepository, ISecurityContext securityContext) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_sessionsRepository = sessionsRepository;
		    _securityContext = securityContext;
		}

		public ActionResult Edit(TimeSlot timeslot)
		{
			if (timeslot == null)
			{
				TempData.Add("message", "Time slot has been deleted.");
				return RedirectToAction<TimeSlotController>(c => c.Index(null));
			}
            if (!_securityContext.HasPermissionsFor(timeslot.Conference))
            {
                return NotAuthorizedView;
            }

			return View(_mapper.Map(timeslot));
		}

		public ActionResult Index(Conference conference)
		{
			TimeSlot[] timeslots = _repository.GetAllForConference(conference);
			ViewData.Add("conferenceKey", conference.Key);
			return View(_mapper.Map(timeslots));
		}

		public ActionResult New(Conference conference)
		{
            if(!_securityContext.HasPermissionsFor(conference))
            {
                return NotAuthorizedView;
            }
			var form = new TimeSlotForm() {ConferenceId = conference.Id, ConferenceKey = conference.Key};
			return View("edit", form);
		}

		[ValidateModel(typeof (TimeSlotForm))]
		public ActionResult Save([Bind(Prefix = "")] TimeSlotForm form, Conference conference,string urlreferrer)
		{
            if(!_securityContext.HasPermissionsFor(conference))
            {
                return NotAuthorizedView;
            }

			Func<TimeSlot, ActionResult> successRedirect = GetSuccessRedirect(conference, urlreferrer);

			return ProcessSave(form, successRedirect);
		}

		public ActionResult Delete(TimeSlot timeslot)
		{
            if(!_securityContext.HasPermissionsFor(timeslot.Conference))
            {
                return NotAuthorizedView;
            }
			if(_sessionsRepository.GetAllForTimeSlot(timeslot).Length==0)
			{
				_repository.Delete(timeslot);
			}
			else
			{
				TempData.Add("message", "Time slot cannot be deleted.");
			}

			return RedirectToAction<TimeSlotController>(c => c.Index(timeslot.Conference));
		}
		private Func<TimeSlot, ActionResult> GetSuccessRedirect(Conference conference, string urlreferrer)
		{
			Func<TimeSlot, ActionResult> successRedirect =
				timeSlot => RedirectToAction<TimeSlotController>(c => c.Index(null), new {conference = conference});

			if (!String.IsNullOrEmpty(urlreferrer))
			{
				successRedirect = timeSlot => Redirect(urlreferrer);
			}
			return successRedirect;
		}
	}
}