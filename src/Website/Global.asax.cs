using System;
using System.Web;
using System.Web.Mvc;
using MvcContrib.ControllerFactories;

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

            RouteTable.Routes.Add(new ControllerRoute("list/[action]", typeof(MvcRouteHandler), "Main", "Index"));
            RouteTable.Routes.Add(new ControllerRoute("helloworld", "Index", "HelloWorld"));
            RouteTable.Routes.Add(new Route("[controller]/[action]", typeof(MvcRouteHandler)));
        }
    }
}