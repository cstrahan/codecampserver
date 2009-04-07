using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public abstract class SmartController : Controller
	{
	    protected ViewResult NotAuthorizedView
	    {
	        get
	        {
	            return View(ViewPages.NotAuthorized);
	        }
	    }
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
		    PageInfo pageInfo = null;

            if (!ViewData.Contains<PageInfo>())
            {   pageInfo = new PageInfo {Title = "Code Camp Server v1.0"};
                ViewData.Add(pageInfo);
            }
            else
            {
                pageInfo = ViewData.Get<PageInfo>();
            }

            if (ViewData.Contains<UserGroup>())
            {
                var usergroup = ViewData.Get<UserGroup>();
                pageInfo.Title = usergroup.Name;

                if (!usergroup.IsDefault())
                    pageInfo.TrackingCode = usergroup.GoogleAnalysticsCode;
            }

            if (ViewData.Contains<Conference>())
            {
                pageInfo.SubTitle = ViewData.Get<Conference>().Name;
            }

		}

        
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			//temporarily putting it here.
			var authentication = new AuthenticationFilterAttribute();
			authentication.OnActionExecuting(filterContext);

			var referrer = new UrlReferrerFilterAttribute();
			referrer.OnActionExecuting(filterContext);

			var version = new AssemblyVersionFilterAttribute();
			version.OnActionExecuting(filterContext);
        
            var usergroup = new AddUserGroupToViewDataActionFilterAttribute();
            usergroup.OnActionExecuting(filterContext);
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