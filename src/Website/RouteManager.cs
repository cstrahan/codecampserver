using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;

namespace CodeCampServer.Website
{
    /// <summary>
    /// Use the RouteManager to define the global routes for the application.    
    /// This class is used in the application itself when registering routes as well as in unit tests for testing the routes.
    /// Taken from: http://haacked.com/archive/2007/12/17/testing-routes-in-asp.net-mvc.aspx
    /// </summary>
    public static class RouteManager
    {
        /// <summary>
        /// Registers the routes for the application.
        /// </summary>
        /// <param name="routes">The container in which to place the routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new Route("Login/[action]",
                new ControllerDefaults("Login", "login"),
                typeof (MvcRouteHandler)));

            routes.Add(new Route("[conferenceKey]/[action]",
                new ControllerDefaults("details", "conference"),
                typeof(MvcRouteHandler)));

            routes.Add(new Route("Default.aspx",
                new
                {
                    action = "details",
                    controller = "conference",
                    conferenceKey = "austincodecamp2008"
                },
                typeof(MvcRouteHandler)));
        }
    }
}
