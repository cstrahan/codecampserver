using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	public class TimeSlotController : SaveController<TimeSlot, TimeSlotForm>
	{
		private readonly ITimeSlotRepository _repository;
		private readonly ITimeSlotMapper _mapper;
		private readonly ISessionRepository _sessionsRepository;

		public TimeSlotController(ITimeSlotRepository repository, ITimeSlotMapper mapper, ISessionRepository sessionsRepository) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_sessionsRepository = sessionsRepository;
		}

		public ActionResult Edit(TimeSlot timeslot)
		{
			if (timeslot == null)
			{
				TempData.Add("message", "Time slot has been deleted.");
				return RedirectToAction<TimeSlotController>(c => c.Index(null));
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
			var form = new TimeSlotForm() {ConferenceId = conference.Id, ConferenceKey = conference.Key};
			return View("edit", form);
		}

		[ValidateModel(typeof (TimeSlotForm))]
		public ActionResult Save([Bind(Prefix = "")] TimeSlotForm form, Conference conference)
		{
			return ProcessSave(form, () => RedirectToAction<TimeSlotController>(c => c.Index(conference)));
		}

		public ActionResult Delete(TimeSlot timeslot)
		{
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
	}
}