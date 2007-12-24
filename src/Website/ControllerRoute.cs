using System;
using System.Web.Mvc;

namespace CodeCampServer.Website
{
    public class ControllerRoute : Route
    {
        public ControllerRoute(string url, Type routeHandler, string defaultAction)
        {
            this.Url = url;
            this.RouteHandler = routeHandler;
            this.Defaults = new ControllerDefaults(defaultAction);
        }

        public ControllerRoute(string url, Type routeHandler, string controller, string defaultAction)
        {
            this.Url = url;
            this.RouteHandler = routeHandler;
            this.Defaults = new ControllerDefaults(defaultAction, controller);
        }

        public ControllerRoute(string url, string defaultAction, string controller)
        {
            this.Url = url;
            this.RouteHandler = typeof (MvcRouteHandler);
            this.Defaults = new ControllerDefaults(defaultAction, controller);
        }
    }
}