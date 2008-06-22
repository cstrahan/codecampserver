using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Castle.Core;
using CodeCampServer.Model;
using CodeCampServer.Website.Helpers;
using MvcContrib.Castle;
using MvcContrib.ExtendedComponentController;

namespace CodeCampServer.Website
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			Log.EnsureInitialized();
			RegisterMvcTypes();
			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.GetContainer()));
			ComponentControllerBuilder.Current.SetComponentControllerFactory(
				new IoCComponentControllerFactory(new WindsorDependencyResolver(IoC.GetContainer())));

			setupRoutes();
		}

		public static void RegisterMvcTypes()
		{
			IoC.Register<IRouteConfigurator, RouteConfigurator>("route-configurator");

			// TODO: windsor mass component registration
			// http://www.kenegozi.com/Blog/2008/03/01/windsor-mass-component-registration.aspx
			// http://mikehadlow.blogspot.com/2008/04/problems-with-mvc-framework.html

			//add all controllers
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (typeof (IController).IsAssignableFrom(type))
				{
					IoC.Register(type.Name.ToLower(), type, LifestyleType.Transient);
				}
				if (typeof (ComponentController).IsAssignableFrom(type))
				{
					IoC.Register(type.Name.ToLower(), type, LifestyleType.Transient);
				}
			}
		}

		private static void setupRoutes()
		{
			var configurator = IoC.Resolve<IRouteConfigurator>();
			configurator.RegisterRoutes();
		}
	}
}