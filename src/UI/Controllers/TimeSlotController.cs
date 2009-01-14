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

		public TimeSlotController(ITimeSlotRepository repository, ITimeSlotMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
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
			_repository.Delete(timeslot);

			return RedirectToAction<TimeSlotController>(c => c.Index(timeslot.Conference));
		}
	}
}