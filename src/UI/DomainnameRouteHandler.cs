using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.UI
{
    public class DomainNameRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.RouteData.Values.Add("UserGroupKey", GetUserGroupKey(requestContext));
            return base.GetHttpHandler(requestContext);
        }

        public string GetUserGroupKey(RequestContext requestContext)
        {
            string key=string.Empty;
            if(requestContext.HttpContext.Request.ServerVariables["HTTP_HOST"]!=null)
            {
                var host = GetHostNameArray(requestContext);
                if(host.Length==1)
                {
                    key = host.First();
                }
                else
                {
                    key = string.Join("_", host.Skip(host.Length - 2).Take(2).ToArray());
                }
            }
            return key;
        }

        private string[] GetHostNameArray(RequestContext requestContext)
        {
            var host = requestContext.HttpContext.Request.ServerVariables["HTTP_HOST"];
            if(host.Contains(":"))
            {
                host = host.Split(':')[0];
            }
            return host.Split('.');
        }
    }
}