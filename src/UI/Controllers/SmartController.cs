using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public abstract class SmartController : Controller
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			//temporarily putting it here.
			var attribute = new AuthenticationFilterAttribute();
			attribute.OnActionExecuting(filterContext);

			var attribute1 = new RequiresConferenceFilterAttribute();
			attribute1.OnActionExecuting(filterContext);

			if (ViewData.Contains<Conference>())
			{
				ViewData["title"] = ViewData.Get<Conference>().Name;
			}
			else
			{
				ViewData["title"] = "Code Camp Server v1.0";
			}
		}

		public RedirectToRouteResult RedirectToAction<TController>(Expression<Func<TController, object>> actionExpression)
		{
			string controllerName = typeof (TController).GetControllerName();
			string actionName = actionExpression.GetActionName();

			return RedirectToAction(actionName, controllerName);
		}

		public RedirectToRouteResult RedirectToAction<TController>(Expression<Func<TController, object>> actionExpression,
		                                                           IDictionary<string, object> dictionary)
		{
			string controllerName = typeof (TController).GetControllerName();
			string actionName = actionExpression.GetActionName();

			return RedirectToAction(actionName, controllerName,
			                        new RouteValueDictionary(dictionary));
		}

		public RedirectToRouteResult RedirectToAction<TController>(Expression<Func<TController, object>> actionExpression,
		                                                           object values)
		{
			string controllerName = typeof (TController).GetControllerName();
			string actionName = actionExpression.GetActionName();

			return RedirectToAction(actionName, controllerName,
			                        new RouteValueDictionary(values));
		}
	}
}