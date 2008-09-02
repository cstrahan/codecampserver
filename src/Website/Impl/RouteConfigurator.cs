﻿using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.Website.Impl
{
	public class RouteConfigurator : IRouteConfigurator
	{
		public virtual void RegisterRoutes()
		{
			var routes = RouteTable.Routes;

            routes.MapRoute("speakers", "{conferenceKey}/speakers",
                new{controller="speaker", action="list"});

            routes.MapRoute("speaker", "{conferenceKey}/speaker/{id}",
                new { controller="speaker", action="show" });            

		    routes.MapRoute("confkey", "{conferenceKey}/{controller}/{action}/{id}",
                new { controller="conference", action="index", id=(string)null },
                new { conferenceKey="(?!conference|admin|login).*"});
            
            routes.MapRoute("standard", "{controller}/{action}/{id}",
                new { controller="conference", action="index", id=(string)null });
			
			routes.MapRoute("default_aspx", "Default.aspx", new {controller = "conference", action = "current"});
		}
	}
}