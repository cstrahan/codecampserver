using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Website.Controllers;
using MvcContrib.ControllerFactories;
using StructureMap;

namespace CodeCampServer.Website
{
	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			Log.EnsureInitialized();
			InitializeControllerFactory();
			SetupRoutes();
		}

		private void InitializeControllerFactory()
		{
			ControllerBuilder.Current.SetDefaultControllerFactory(typeof (StructureMapControllerFactory));
			StructureMapConfiguration.BuildInstancesOf<ConferenceController>()
				.TheDefaultIsConcreteType<ConferenceController>();
			StructureMapConfiguration.BuildInstancesOf<LoginController>().TheDefaultIsConcreteType<LoginController>();
		}

		private void SetupRoutes()
		{
			RouteManager.RegisterRoutes(RouteTable.Routes);
		}
	}
}