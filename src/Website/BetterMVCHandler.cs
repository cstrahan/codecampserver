using System;
using System.Web;
using System.Web.Mvc;

namespace CodeCampServer.Website
{
	public class BetterMvcHandler : MvcHandler
	{
		protected override void ProcessRequest(IHttpContext httpContext)
		{
			try
			{
				base.ProcessRequest(httpContext);
			}
			catch (Exception ex)
			{
				throw new Exception("Route used:" + RequestContext.RouteData.Route.Url, ex);
			}
		}
	}
}