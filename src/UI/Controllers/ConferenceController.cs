using System;
using System.Collections.Generic;
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
	public class ConferenceController : SaveController<Conference, ConferenceForm>
	{
		private readonly IConferenceRepository _repository;
		private readonly IConferenceMapper _mapper;

		public ConferenceController(IConferenceRepository repository, IConferenceMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
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

			return View(_mapper.Map(conference));
		}

		[ValidateModel(typeof (ConferenceForm))]
		public ActionResult Save([Bind(Prefix = "")] ConferenceForm form)
		{
			return ProcessSave(form, () => RedirectToAction<ConferenceController>(c => c.Index()));
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(ConferenceForm form)
		{
			var result = new ValidationResult();
			if (ConferenceKeyAlreadyExists(form))
			{
				result.AddError<ConferenceForm>(x => x.Key, "This conference key already exists");
			}
			return result.GetAllErrors();
		}

		private bool ConferenceKeyAlreadyExists(ConferenceForm message)
		{
			Conference conference = _repository.GetByKey(message.Key);
			return conference != null && conference.Id != message.Id;
		}

		public ActionResult New()
		{
			var conference = new Conference {StartDate = SystemTime.Now(), EndDate = SystemTime.Now()};
			_repository.Save(conference);
			return View("Edit", _mapper.Map(conference));
		}
	}
}