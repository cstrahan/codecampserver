using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	public class SpeakerController : SaveController<Speaker, SpeakerForm>
	{
		private readonly ISpeakerRepository _repository;
		private readonly ISpeakerMapper _mapper;

		public SpeakerController(ISpeakerRepository repository, ISpeakerMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public ActionResult Index()
		{
			var speakers = _repository.GetAll();
			return View(_mapper.Map(speakers));
		}

		public ActionResult Edit(Speaker speaker)
		{
			if (speaker == null)
			{
				TempData.Add("message", "Speaker has been deleted.");
				return RedirectToAction<SpeakerController>(c => c.Index());
			}
			return View(_mapper.Map(speaker));
		}

		public ActionResult Save([Bind(Prefix = "")] SpeakerForm form)
		{
			return ProcessSave(form, () => RedirectToAction<SpeakerController>(c => c.Index()));
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(SpeakerForm form)
		{
			var result = new ValidationResult();
			if (SpeakerKeyAlreadyExists(form))
			{
				result.AddError<SpeakerForm>(x => x.Key, "This speaker key already exists");
			}

			return result.GetAllErrors();
		}

		private bool SpeakerKeyAlreadyExists(SpeakerForm message)
		{
			Speaker speaker = _repository.GetByKey(message.Key);
			return speaker != null && speaker.Id != message.Id;
		}

		public ActionResult New()
		{
			return View("Edit", new SpeakerForm());
		}

		public ActionResult Delete(Speaker speaker)
		{
			_repository.Delete(speaker);
			return RedirectToAction<SpeakerController>(c => c.Index());
		}
	}
}