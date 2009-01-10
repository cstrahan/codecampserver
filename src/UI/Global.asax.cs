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

			new RouteConfigurator().RegisterRoutes();
		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
		}
	}
}