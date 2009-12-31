using System;
using System.Web;

namespace CodeCampServer.UI.Helpers
{
	public class PreviousViewPageUrlManager : IPreviousViewPageUrlManager
	{
		public const string ParameterName = "previousViewPageUrl";

		public string GetPreviousViewPageUrl()
		{
			var url = HttpContext.Current.Request.Params[ParameterName];

			if (string.IsNullOrEmpty(url))
			{
				throw new ApplicationException(
					"The Previous View Page URL has not been set.  Check the previous page to make sure the hyperlink to this page includes previousViewPageUrl as a query string or form parameter.");
			}

			return url;
		}

		public bool HasPreviousViewPageUrl()
		{
			var url = HttpContext.Current.Request.Params[ParameterName];
			return !string.IsNullOrEmpty(url);
		}

		public string GetTargetUrlWithPreviousPage(string targetUrl)
		{
			string url = HttpContext.Current.Request.RawUrl;

			var targetUrlContainsParameter = targetUrl.Contains("?");
			string delimiter = targetUrlContainsParameter ? "&amp;" : "?";

			targetUrl += string.Format("{0}{1}={2}", delimiter, ParameterName, HttpUtility.UrlEncode(url));
			return targetUrl;
		}
	}
}