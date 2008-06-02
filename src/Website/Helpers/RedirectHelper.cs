using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.Website.Helpers
{
    public static class RedirectHelper
    {
        public static ActionResult RedirectToLogin(HttpRequestBase request)
        {
            var url = request.Url.PathAndQuery;

            return new RedirectToRouteResult(new RouteValueDictionary(new {
                controller = "login", action = "index", redirectUrl = url
            }));
        }
    }
}