using System.Web.Routing;
using MvcContrib.Routing;

namespace CodeCampServer.UI
{
    public class RouteConfigurator 
    {

        public virtual void RegisterRoutes()
        {
            RouteCollection routes = RouteTable.Routes;

            routes.Clear();

            MvcRoute.MappUrl("{conferenceKey}/speakers/{speakerKey}")
                .WithDefaults(new {controller = "Speaker", action = "index"})
                .AddWithName("speaker", routes)
                .RouteHandler = new DomainNameRouteHandler();

            MvcRoute.MappUrl("{conferenceKey}/sessions/{sessionKey}")
                .WithDefaults(new {controller = "Session", action = "index"})
                .AddWithName("session", routes)
                .RouteHandler = new DomainNameRouteHandler();

            MvcRoute.MappUrl("{conferenceKey}/{controller}/{action}")
                .WithDefaults(new {controller = "Conference", action = "index"})
                .WithConstraints(new
                                     {
                                         conferenceKey = new ConferenceKeyCannotBeAControllerNameContraint(),
                                         controller =
                                     "schedule|session|timeslot|track|attendee|conference|speaker|admin|proposal"
                                     })
                .AddWithName("conferenceDefault", routes)
                .RouteHandler = new DomainNameRouteHandler();

            MvcRoute.MappUrl("{controller}/{action}")
                .WithDefaults(new {controller = "home", action = "index"})
                .WithConstraints(new
                                     {
                                         controller = "(admin|login|speaker|home|conference|usergroup)"
                                     })
                .AddWithName("default", routes)
                .RouteHandler = new DomainNameRouteHandler();
        }

    }
}