using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;

namespace CodeCampServer.Website
{
    public class BetterMVCHandler : MvcHandler
    {
        protected override void ProcessRequest(IHttpContext httpContext)
        {
            try
            {
                base.ProcessRequest(httpContext);
            }
            catch(Exception ex)
            {
                throw new Exception("Route used:" + this.RequestContext.RouteData.Route.Url.ToString(), ex);
            }
        } 
    }
}
