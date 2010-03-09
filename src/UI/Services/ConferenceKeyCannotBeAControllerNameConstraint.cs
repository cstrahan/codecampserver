using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace CodeCampServer.UI.Services
{
	public class ConferenceKeyCannotBeAControllerNameConstraint : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
		                  RouteDirection routeDirection)
		{
			if (parameterName == "conferenceKey")
			{
				string conferenceKeyValue = values[parameterName].ToString();

				IEnumerable<Type> controllers = GetAllControllersFromThisAssembly();
				return NumberOfControllersThatHaveTheName(controllers, conferenceKeyValue) == 0;
			}
			return false;
		}

		private int NumberOfControllersThatHaveTheName(IEnumerable<Type> controllers, string value)
		{
			return
				controllers.Count(
					c => string.Compare(c.Name.Replace("Controller", ""), value, StringComparison.OrdinalIgnoreCase) == 0);
		}

		private IEnumerable<Type> GetAllControllersFromThisAssembly()
		{
			return GetType().Assembly.GetTypes().Where(t => IsAController(t));
		}

		private bool IsAController(Type e)
		{
			return e.GetInterface("IController") != null;
		}
	}
}