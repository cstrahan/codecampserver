using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI.Models;

namespace CodeCampServer.UI
{
	public class GlobalApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.gif/{*pathInfo}");

			routes.MapRoute("Home", "home", new {controller = "home", action = "index"});
            
			routes.MapRoute(
				"Enumerations", // Route name
				"Enumerations", // URL with parameters
				new {controller = "enumeration", action = "viewenumerations"} // Parameter defaults
				);

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}", // URL with parameters
				new {controller = "home", action = "index"} // Parameter defaults
				);
		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
		}
	}
}