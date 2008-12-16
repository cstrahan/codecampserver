using System;
using System.Linq.Expressions;

namespace CodeCampServer.UI
{
	public static class MvcHelperExtensions
	{
		public static string GetControllerName(this Type controllerType)
		{
			return controllerType.Name.Replace("Controller", string.Empty);
		}

		public static string GetActionName(this LambdaExpression actionExpression)
		{
			return ((MethodCallExpression)actionExpression.Body).Method.Name;
		}
	}
}