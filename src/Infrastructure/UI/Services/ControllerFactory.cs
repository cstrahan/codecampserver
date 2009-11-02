using System;
using System.Web.Mvc;
using StructureMap;

namespace CodeCampServer.Infrastructure.UI.Services
{
	public class ControllerFactory : DefaultControllerFactory
	{

		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
		{
			if (controllerType != null)
			{
				return (IController)ObjectFactory.GetInstance(controllerType);
			}
			return null;
		}
	}
}