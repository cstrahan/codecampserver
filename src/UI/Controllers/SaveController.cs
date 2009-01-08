using System;
using System.Web.Mvc;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Helpers;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UI.Controllers
{
	public abstract class SaveController<TModel, TMessage> : SmartController
		where TModel : PersistentObject
	{
		protected abstract IModelUpdater<TModel, TMessage> Updater { get; }

		protected ActionResult ProcessSave(TMessage message, Func<TModel, RedirectToRouteResult> successRedirect,
		                                   Action beforeFailAction)
		{
			if (!ModelState.IsValid)
			{
				beforeFailAction();

				return View("Edit", message);
			}

			UpdateResult<TModel, TMessage> result = Updater.UpdateFromMessage(message);

			if (!result.Successful)
			{
				beforeFailAction();

				ModelState.AddModelErrors(result.GetAllMessages());

				return View("Edit", message);
			}

			return successRedirect(result.Model);
		}

		protected ActionResult ProcessSave(TMessage message, Func<TModel, RedirectToRouteResult> successRedirect)
		{
			return ProcessSave(message, successRedirect, () => { });
		}

		protected ActionResult ProcessSave(TMessage message, Func<RedirectToRouteResult> successRedirect)
		{
			return ProcessSave(message, model => successRedirect(), () => { });
		}

		protected ActionResult ProcessSave(TMessage message, Func<RedirectToRouteResult> successRedirect,
		                                   Action beforeFailAction)
		{
			return ProcessSave(message, model => successRedirect(), beforeFailAction);
		}
	}
}