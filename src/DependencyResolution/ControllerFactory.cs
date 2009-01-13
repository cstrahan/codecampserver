using System;
using System.Web.Mvc;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;

namespace CodeCampServer.DependencyResolution
{
	public class ControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(Type controllerType)
		{
			Logger.Debug(this, string.Format("Creating controller {0}", controllerType.Name));
			return (IController) ObjectFactory.GetInstance(controllerType);
		}
	}
}