using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UI
{
	public class ControllerFactory : DefaultControllerFactory
	{
		public static Func<Type, object> CreateDependencyCallback = (type) => Activator.CreateInstance(type);

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType != null)
			{
				var controller = (Controller) CreateDependencyCallback(controllerType);
				controller.ActionInvoker = (IActionInvoker) CreateDependencyCallback(typeof (ConventionActionInvoker));
				return controller;
			}
			return null;
		}
	}
}