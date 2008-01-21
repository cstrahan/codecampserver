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
			initializeControllerFactory();
			setupRoutes();
		}

		private void initializeControllerFactory()
		{
			ControllerBuilder.Current.SetDefaultControllerFactory(typeof (StructureMapControllerFactory));

			//StructureMapControllerFactory will be enhanced in MvcContrib to make the following unecessary.
			StructureMapConfiguration.BuildInstancesOf<ConferenceController>().TheDefaultIsConcreteType<ConferenceController>();
			StructureMapConfiguration.BuildInstancesOf<LoginController>().TheDefaultIsConcreteType<LoginController>();
			StructureMapConfiguration.BuildInstancesOf<SpeakerController>().TheDefaultIsConcreteType<SpeakerController>();
			StructureMapConfiguration.BuildInstancesOf<ScheduleController>().TheDefaultIsConcreteType<ScheduleController>();
		}

		private void setupRoutes()
		{
			IRouteConfigurator configurator = ObjectFactory.GetInstance<IRouteConfigurator>();
			configurator.RegisterRoutes();
		}
	}
}