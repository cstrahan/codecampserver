using System;
using System.Web.Mvc;
using CodeCampServer.Model;
using StructureMap;

namespace CodeCampServer.Website
{
	[Pluggable(Keys.DEFAULT)]
	public class RouteConfigurator : IRouteConfigurator
	{
		public virtual void RegisterRoutes()
		{
			RouteCollection routes = RouteTable.Routes;
			routes.Add(new Route("Login/[action]",
				new ControllerDefaults("Login", "login"),
				typeof(BetterMvcRouteHandler)));
                        
            routes.Add(new Route
            {
                Url = "[conferenceKey]/speakers",
                Defaults = new { Controller = "speaker", Action = "list" },
                RouteHandler = typeof(BetterMvcRouteHandler)
            });

            routes.Add(new Route
		    {
		        Url = "[conferenceKey]/speaker/[speakerId]",
                Defaults = new { Controller = "speaker", Action = "view" },
                RouteHandler = typeof(BetterMvcRouteHandler)
		    });

			routes.Add(new Route
			{
				Url = "[conferenceKey]/schedule/[action]",
				Defaults = new { Controller = "schedule", Action = "index" },
				RouteHandler = typeof(BetterMvcRouteHandler)
			});

			routes.Add(new Route("[conferenceKey]/[action]",   
				new ControllerDefaults("index", "conference"),
				typeof(BetterMvcRouteHandler)));

			routes.Add(new Route("Default.aspx",
				new
				{
					action = "details",
					controller = "conference",
					conferenceKey = "austincodecamp2008"
				},
				typeof(BetterMvcRouteHandler)));
		}
	}
}
