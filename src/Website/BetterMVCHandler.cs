using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.Website
{
	public class BetterMvcHandler : MvcHandler
	{
		public BetterMvcHandler(RequestContext context) : base(context)
		{
			
		}

		protected override void ProcessRequest(HttpContext httpContext)
		{
			try
			{
				base.ProcessRequest(httpContext);
			}
			catch (Exception ex)
			{
				throw new Exception("Route used:" + RequestContext.RouteData.Route, ex);
			}
		}
	}
}