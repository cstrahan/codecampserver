using System;
using System.Web;

namespace CodeCampServer.UI.Services
{
	public class ReturnUrlManager : IReturnUrlManager
	{
		public const string ParameterName = "ReturnToUrl";

		public string GetReturnUrl()
		{
			var url = HttpContext.Current.Request.Params[ParameterName];

			if (string.IsNullOrEmpty(url))
			{
				throw new ApplicationException(string.Format("The Return URL has not been set.  Check the previous page to make sure the hyperlink to this page includes {0} as a query string or form parameter.", ParameterName));
			}

			return url;
		}

		public bool HasReturnUrl()
		{
			var url = HttpContext.Current.Request.Params[ParameterName];
			return !string.IsNullOrEmpty(url);
		}

		public string GetTargetUrlWithReturnUrl(string targetUrl)
		{
			string url = HttpContext.Current.Request.RawUrl;

			var targetUrlContainsParameter = targetUrl.Contains("?");
			string delimiter = targetUrlContainsParameter ? "&amp;" : "?";

			targetUrl += string.Format("{0}{1}={2}", delimiter, ParameterName, HttpUtility.UrlEncode(url));
			return targetUrl;
		}

		public string GetCurrentUrl()
		{
			return HttpContext.Current.Request.RawUrl;
		}
	}
}