using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.Website
{
	public class RouteConfigurator : IRouteConfigurator
	{
		public virtual void RegisterRoutes()
		{		    
			RouteCollection routes = RouteTable.Routes;        

			routes.Add(new Route("login/{action}",
			                     new RouteValueDictionary(new {Controller = "login", Action = "index"}),
			                     new MvcRouteHandler()));

            routes.Add(new Route("admin/{action}",
                                 new RouteValueDictionary(new{Controller="Admin", Action="index"}),
                                 new MvcRouteHandler()));

			routes.Add(new Route("{conferenceKey}/speaker/{speakerId}",
								 new RouteValueDictionary(new { Controller = "speaker", Action = "view" }),
								 new MvcRouteHandler()));


		    routes.Add(new Route("{conferenceKey}/speakers/{action}",
                                new RouteValueDictionary(new {controller = "speaker", action = "list"}),
                                new MvcRouteHandler()));

                routes.Add(new Route("{conferenceKey}/schedule/{action}",
                                    new RouteValueDictionary(new { controller = "schedule", action = "index" }),
                                    new MvcRouteHandler()));

		    routes.Add(new Route("{conferenceKey}/sessions/{action}",
		                        new RouteValueDictionary(new {Controller = "session", Action = "list"}),
		                        new MvcRouteHandler()));

		    routes.Add(new Route("{conferenceKey}/sponsors/{action}",
		                        new RouteValueDictionary(new {Controller = "sponsor", Action = "list"}),
		                        new MvcRouteHandler()));

		    routes.Add(new Route("{conferenceKey}/sponsors/{action}/{sponsorName}",
		                        new RouteValueDictionary(new {Controller = "sponsor", Action = "edit"}),
		                        new MvcRouteHandler()));

            routes.Add(new Route("{conferenceKey}/{action}",
                                new RouteValueDictionary(new { controller = "conference", action = "index" }),
                                new MvcRouteHandler()));

		    routes.Add(new Route("Default.aspx",
		                         new RouteValueDictionary(new { action = "current", controller = "conference" }),
		                         new MvcRouteHandler()));				
		}
	}
}
