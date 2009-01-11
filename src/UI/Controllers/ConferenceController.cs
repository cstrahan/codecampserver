using System;
using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.AutoMap;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class ConferenceController : SaveController<Conference, IConferenceMessage>
	{
		private readonly IConferenceRepository _repository;
		private readonly IConferenceUpdater _updater;

		public ConferenceController(IConferenceRepository repository, IConferenceUpdater updater)
		{
			_repository = repository;
			_updater = updater;
		}

		public ActionResult Index()
		{
			Conference[] conferences = _repository.GetAll();

			if (conferences.Length < 1)
			{
				return RedirectToAction<ConferenceController>(c => c.New());
			}

			object conferenceListDto = AutoMapper.Map(conferences, typeof (Conference[]), typeof (ConferenceForm[]));
			return View(conferenceListDto);
		}

		public ActionResult Edit(Guid Id)
		{
			Conference conference = _repository.GetById(Id);

			if (conference == null)
			{
				TempData.Add("message", "Conference has been deleted.");
				return RedirectToAction<ConferenceController>(c => c.Index());
			}

			var form = (ConferenceForm) AutoMapper.Map(conference, typeof (Conference), typeof (ConferenceForm));

			return View(form);
		}

		[ValidateModel(typeof (ConferenceForm))]
		public ActionResult Save([Bind(Prefix = "")] ConferenceForm form)
		{
			return ProcessSave(form, () => RedirectToAction<ConferenceController>(c => c.Index()));
		}

		public ActionResult New()
		{
			var conference = new Conference {StartDate = SystemTime.Now(), EndDate = SystemTime.Now()};
			_repository.Save(conference);
			var form = (ConferenceForm) AutoMapper.Map(conference, typeof (Conference), typeof (ConferenceForm));
			return View("Edit", form);
		}

		protected override IModelUpdater<Conference, IConferenceMessage> GetUpdater()
		{
			return _updater;
		}
	}
}