using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.UI.Helpers;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Controllers
{
	public abstract class SaveController<TModel, TForm> : SmartController
		where TModel : PersistentObject, new()
	{
		private readonly IRepository<TModel> _repository;
		private readonly IMapper<TModel, TForm> _mapper;

		protected SaveController(IRepository<TModel> repository, IMapper<TModel, TForm> mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		protected ActionResult ProcessSave(TForm form, Func<ActionResult> successRedirect)
		{
			if (!ModelState.IsValid)
			{
				return View("Edit", form);
			}

			ModelState.AddModelErrors(GetFormValidationErrors(form));
			if (!ModelState.IsValid)
			{
				return View("Edit", form);
			}

			TModel model = _mapper.Map(form);
			ModelState.AddModelErrors(GetValidationErrors(model));
			if (!ModelState.IsValid)
			{
				return View("Edit", form);
			}

			_repository.Save(model);
			return successRedirect();
		}

		protected virtual IDictionary<string, string[]> GetFormValidationErrors(TForm form)
		{
			return new Dictionary<string, string[]>();
		}

		protected virtual IDictionary<string, string[]> GetValidationErrors(TModel model)
		{
			return new Dictionary<string, string[]>();
		}
	}
}