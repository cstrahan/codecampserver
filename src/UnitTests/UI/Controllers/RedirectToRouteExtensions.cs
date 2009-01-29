using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CodeCampServer.UI;

namespace CodeCampServer.UnitTests.UI.Controllers
{
	public static class RedirectToRouteResultExtensions
	{
		public static bool RedirectsTo<TController>(this RedirectToRouteResult result,
		                                            Expression<Func<TController, object>> actionExpression)
		{
			string controllerName = typeof (TController).GetControllerName();
			string actionName = actionExpression.GetActionName();

			if (!result.RouteValues.ContainsKey("action") || !result.RouteValues.ContainsKey("controller"))
				return false;

			return (((string) result.RouteValues["action"]).Equals(actionName, StringComparison.CurrentCultureIgnoreCase) &&
			        ((string) result.RouteValues["controller"]).Equals(controllerName, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}