using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCampServer.Website
{
	public class BetterMvcRouteHandler : IRouteHandler
	{
		#region IRouteHandler Members

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			MvcHandler handler = new BetterMvcHandler(requestContext);
			return handler;
		}

		#endregion
	}
}