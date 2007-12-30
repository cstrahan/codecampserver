using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Website.Controllers;
using MvcContrib.ControllerFactories;
using StructureMap;

namespace CodeCampServer.Website
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            SetupRoutes();
        }

        private void SetupRoutes()
        {
            ControllerBuilder.Current.SetDefaultControllerFactory(typeof(StructureMapControllerFactory));
            StructureMapConfiguration.BuildInstancesOf<ConferenceController>()
                .TheDefaultIsConcreteType<ConferenceController>();

            RouteTable.Routes.Add(new Route("[conferenceKey]/[action]", 
                new ControllerDefaults("details", "conference"), 
                typeof(MvcRouteHandler)));
            
            RouteTable.Routes.Add(new Route("Default.aspx", 
                new {action = "details", 
                    controller = "conference", 
                    conferenceKey = "austincodecamp2008"},
                typeof(MvcRouteHandler)));
        }
    }
}