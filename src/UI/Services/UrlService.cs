using System.Web;

namespace CodeCampServer.UI.Services
{
	public class UrlService : IUrlService
	{

		public string UrlReferrer
		{
			get
			{
				HttpContext context = HttpContext.Current;
				if (theUrlReferrerIsNotNull(context))
					return context.Request.UrlReferrer.PathAndQuery;
				return string.Empty;
			}
		}


		private bool theUrlReferrerIsNotNull(HttpContext context)
		{
			return context != null && (context.Request != null && context.Request.UrlReferrer != null);
		}
	}
}