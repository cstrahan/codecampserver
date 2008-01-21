using System.Web;
using System.Web.Mvc;

namespace CodeCampServer.Website
{
	public class BetterMvcRouteHandler : IRouteHandler
	{
		#region IRouteHandler Members

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			MvcHandler handler = new BetterMvcHandler();
			handler.RequestContext = requestContext;
			return handler;
		}

		#endregion
	}
}