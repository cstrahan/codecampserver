using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public class TimeSlotController : SaveController<TimeSlot, ITimeSlotMessage>
	{
		private readonly ITimeSlotRepository _repository;
		private readonly ITimeSlotUpdater _updater;

		public TimeSlotController(ITimeSlotRepository repository, ITimeSlotUpdater updater)
		{
			_repository = repository;
			_updater = updater;
		}

		protected override IModelUpdater<TimeSlot, ITimeSlotMessage> GetUpdater()
		{
			return _updater;
		}

		[AutoMappedToModelFilter(typeof (TimeSlot), typeof (TimeSlotForm))]
		public ActionResult Edit(TimeSlot timeslot)
		{
			if (timeslot == null)
			{
				TempData.Add("message", "Time slot has been deleted.");
				return RedirectToAction<TimeSlotController>(c => c.Index(null));
			}
			ViewData.Add(timeslot);
			return View();
		}

		[AutoMappedToModelFilter(typeof (TimeSlot[]), typeof (TimeSlotForm[]))]
		public ActionResult Index(Conference conference)
		{
			TimeSlot[] timeslots = _repository.GetAllForConference(conference);
			ViewData.Add(timeslots);
			ViewData.Add("conferenceKey", conference.Key);
			return View();
		}

		[AutoMappedToModelFilter(typeof (TimeSlot), typeof (TimeSlotForm))]
		public ActionResult New(Conference conference)
		{
			var timeSlot = new TimeSlot {Conference = conference};
			ViewData.Add(timeSlot);
			return View("edit");
		}

		[ValidateModel(typeof(TimeSlotForm))]
		public ActionResult Save([Bind(Prefix = "")]TimeSlotForm form, Conference conference)
		{
			return ProcessSave(form, () => RedirectToAction<TimeSlotController>(c => c.Index(conference)));
		}
	}
}
