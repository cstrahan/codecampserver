using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UI
{
    public class DomainNameRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.RouteData.Values.Add("domainname", requestContext.HttpContext.Request.Url.Host);
            return base.GetHttpHandler(requestContext);
        }
    }
}