using System;
using System.Web.Mvc;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class ControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(Type controllerType)
		{
			if(controllerType!=null)
			{
				//Logger.Debug(this, string.Format("Creating controller {0}", controllerType.Name));
				return (IController)ObjectFactory.GetInstance(controllerType);
			}
			return null;
		}
	}
}