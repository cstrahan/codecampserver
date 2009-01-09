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

			if (!result.Values.ContainsKey("action") || !result.Values.ContainsKey("controller"))
				return false;

			return (((string) result.Values["action"]).Equals(actionName, StringComparison.CurrentCultureIgnoreCase) &&
			        ((string) result.Values["controller"]).Equals(controllerName, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}