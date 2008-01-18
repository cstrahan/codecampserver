using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Website.Controllers;
using MvcContrib.ControllerFactories;
using StructureMap;
using System.Reflection;

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
			
            /*add all controllers from this assembly
            foreach(Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(type.IsClass && !type.IsAbstract && type.IsAssignableFrom(typeof(IController)))
                {                    
                    StructureMapConfiguration.BuildInstancesOf<type>().TheDefaultIsConcreteType<type>();
                }
            }*/

            StructureMapConfiguration.BuildInstancesOf<ConferenceController>().TheDefaultIsConcreteType<ConferenceController>();
			StructureMapConfiguration.BuildInstancesOf<LoginController>().TheDefaultIsConcreteType<LoginController>();
		    StructureMapConfiguration.BuildInstancesOf<SpeakerController>().TheDefaultIsConcreteType<SpeakerController>();
		}

		private void SetupRoutes()
		{
			RouteManager.RegisterRoutes(RouteTable.Routes);
		}
	}
}