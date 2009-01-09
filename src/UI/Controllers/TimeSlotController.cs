using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Filters;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.UI.Models.ViewModels;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public class TimeSlotController:SaveController<TimeSlot,ITimeSlotMessage>
	{
		private readonly ITimeSlotRepository _repository;
		private readonly ITimeSlotUpdater _updater;

		public TimeSlotController(ITimeSlotRepository repository, ITimeSlotUpdater updater)
		{
			_repository = repository;
			_updater = updater;
		}

		protected override IModelUpdater<TimeSlot, ITimeSlotMessage> Updater
		{
			get { return _updater; }
		}

		[AutoMappedToModelFilter(typeof(TimeSlot), typeof(TimeSlotForm))]
		public ActionResult Edit(TimeSlot timeslot)
		{
			if(timeslot==null)
			{
				TempData.Add("message", "Time slot has been deleted.");
				return RedirectToAction<TimeSlotController>(c => c.Index(null));				
			}
			ViewData.Add(timeslot);
			return View();
		}

		[AutoMappedToModelFilter(typeof(TimeSlot[]), typeof(TimeSlotForm[]))]
		public ActionResult Index(Conference conference)
		{
			var timeslots = _repository.GetAllForConference(conference);
			ViewData.Add(timeslots);
			return View();
		}

		public ActionResult New(Conference conference)
		{
			var form = new TimeSlotForm() {ConferenceId = conference.Id};
			return View("edit",form);
		}

		[ValidateModel(typeof(TrackForm))]
		public ActionResult Save([Bind(Prefix = "")]TimeSlotForm form, Conference conference)
		{
			return ProcessSave(form, () => RedirectToAction<TimeSlotController>(c => c.Index(conference)));
		}
	}
}