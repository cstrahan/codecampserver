using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UI.Helpers.Extensions
{
	public static class UrlHelperExtensions
	{
		public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, object>> actionExpression)
		{
			var controllerName = typeof(TController).GetControllerName();
			var actionName = actionExpression.GetActionName();

			return urlHelper.Action(actionName, controllerName);
		}

		public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, object>> actionExpression, RouteValueDictionary routeValueDictionary)
		{
			var controllerName = typeof(TController).GetControllerName();
			var actionName = actionExpression.GetActionName();

			return urlHelper.Action(actionName, controllerName, routeValueDictionary);
		}
	}
}