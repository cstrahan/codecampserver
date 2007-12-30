using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Website.Controllers;
using MvcContrib.ControllerFactories;
using StructureMap;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            InitializeControllerFactory();
            SetupRoutes();
        }

        private void InitializeControllerFactory()
        {
            ControllerBuilder.Current.SetDefaultControllerFactory(typeof(StructureMapControllerFactory));
            StructureMapConfiguration.BuildInstancesOf<ConferenceController>()
                .TheDefaultIsConcreteType<ConferenceController>();
        }

        private void SetupRoutes()
        {
            RouteManager.RegisterRoutes(RouteTable.Routes);            
        }
    }
}
