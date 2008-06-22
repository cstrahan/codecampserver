using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.Website
{
	public class RouteConfigurator : IRouteConfigurator
	{
		public virtual void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;

			routes.MapRoute("login", "login/{action}", new {controller = "login", action = "index"});

			//explicit conference routes that don't need a conference key
			routes.MapRoute("new-conference", "conference/new", new {controller = "conference", action = "new"});
			routes.MapRoute("list-conferences", "conference/list", new {controller = "conference", action = "list"});
			routes.MapRoute("save-conferences", "conference/save", new {controller = "conference", action = "save"});
			routes.MapRoute("conference-keycheck", "conference/keycheck/{conferenceKey}",
			                new {controller = "conference", action = "keycheck"});

			routes.MapRoute("admin", "admin/{action}", new {controller = "admin", action = "index"});
			routes.MapRoute("speaker", "{conferenceKey}/speaker/{speakerId}",
			                new {controller = "speaker", action = "view"});
			routes.MapRoute("speakers", "{conferenceKey}/speakers/{action}",
			                new {controller = "speaker", action = "list"});
			routes.MapRoute("schedule", "{conferenceKey}/schedule/{action}",
			                new {controller = "schedule", action = "index"});
			routes.MapRoute("sessions", "{conferenceKey}/sessions/{action}",
			                new {controller = "session", action = "list"});
			routes.MapRoute("sponsors", "{conferenceKey}/sponsors/{action}",
			                new {controller = "sponsor", action = "list"});
			routes.MapRoute("sponsor-edit", "{conferenceKey}/sponsors/{action}/{sponsorName}",
			                new {controller = "sponsor", action = "edit"});
			routes.MapRoute("conference", "{conferenceKey}/{action}",
			                new {controller = "conference", action = "details"});
			routes.MapRoute("default_aspx", "Default.aspx", new {controller = "conference", action = "current"});
		}
	}
}