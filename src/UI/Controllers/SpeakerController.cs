using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;
namespace CodeCampServer.UI.Controllers
{
	public class SpeakerController:SaveController<Speaker,ISpeakerMessage>
	{
		private readonly ISpeakerRepository _repository;
		private readonly ISpeakerUpdater _updater;

		public SpeakerController(ISpeakerRepository repository, ISpeakerUpdater updater)
		{
			_repository = repository;
			_updater = updater;
		}

		[AutoMappedToModelFilter(typeof(Speaker[]), typeof(SpeakerForm[]))]
		public ActionResult Index()
		{
			var speakers = _repository.GetAll();
			ViewData.Add(speakers);
			return View();
		}

		protected override IModelUpdater<Speaker, ISpeakerMessage> GetUpdater()
		{
			return _updater;
		}

		[AutoMappedToModelFilter(typeof(Speaker), typeof(SpeakerForm))]
		public ActionResult Edit(Speaker speaker)
		{
			if (speaker == null)
			{
				TempData.Add("message", "Speaker has been deleted.");
				return RedirectToAction<SpeakerController>(c => c.Index());
			}
			ViewData.Add(speaker);
			return View();			
		}

		public ActionResult Save(SpeakerForm form)
		{
			return ProcessSave(form,()=>RedirectToAction<SpeakerController>(c => c.Index()));
		}

		public ActionResult New()
		{

			return View("Edit",new SpeakerForm());
		}

		public ActionResult Delete(Speaker speaker)
		{
			_repository.Delete(speaker);
			return RedirectToAction<SpeakerController>(c => c.Index());
		}
	}
}