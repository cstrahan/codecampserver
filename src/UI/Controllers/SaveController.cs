using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers;

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

		protected ActionResult ProcessSave(TForm form, Func<TModel, ActionResult> successRedirect)
		{
		    try
		    {
                return ProcessSave(form, successRedirect, model => { });
		    }
		    catch (Exception e)
		    {
		        while (e.InnerException!=null)
		        {
		            e = e.InnerException;
		        }
                ModelState.AddModelError("form",e.Message);
		        return View("edit",form);
		    }
			
		}

		protected ActionResult ProcessSave(TForm form, Func<TModel, ActionResult> successRedirect, Action<TModel> preSaveAction)
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

			preSaveAction(model);
			if (!ModelState.IsValid)
			{
				return View("Edit", form);
			}

			_repository.Save(model);
			return successRedirect(model);
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