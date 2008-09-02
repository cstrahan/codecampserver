using System;
using System.Web.Mvc;

namespace CodeCampServer.Website.Helpers
{
	public static class ViewPageHelpers
	{
		/// <summary>
		/// Displays an existing temporary message (aka a "flash") if one exists.
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		public static string DisplayFlashMessage(this ViewPage page)
		{
			return page.ViewContext.TempData["message"] as string ?? string.Empty;
		}

		/// <summary>
		/// Displays an existing temporary message (aka a "flash") if one exists.
		/// </summary>
		/// <param name="masterPage"></param>
		/// <returns></returns>
		public static string DisplayFlashMessage(this ViewMasterPage masterPage)
		{
			return masterPage.ViewContext.TempData["message"] as string ?? string.Empty;
		}

		public static bool HasFlashMessage(this ViewPage page)
		{
			return page.ViewContext.TempData["message"] != null;
		}

		public static bool HasFlashMessage(this ViewMasterPage masterPage)
		{
			return masterPage.ViewContext.TempData["message"] != null;
		}

        public static string ResolveUrl(this HtmlHelper html, string relativeUrl)
        {
            if (relativeUrl == null)
                return null;
                        
            if (! relativeUrl.StartsWith("~"))
                return relativeUrl;

            var basePath = html.ViewContext.HttpContext.Request.ApplicationPath;
            string url = basePath + relativeUrl.Substring(1);
            return url.Replace("//", "/");     
        }

        public static string Stylesheet(this HtmlHelper html, string cssFile)
        {
            string cssPath = cssFile.Contains("~") ? cssFile : "~/content/css/" + cssFile;
            string url = ResolveUrl(html, cssPath);
            return string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" />\n", url);
        }

        public static string ScriptInclude(this HtmlHelper html, string jsFile)
        {
            string jsPath = jsFile.Contains("~") ? jsFile : "~/content/js/" + jsFile;
            string url = ResolveUrl(html, jsPath);
            return string.Format("<script type=\"text/javascript\" src=\"{0}\" ></script>\n", url);
        }

        public static string Favicon(this HtmlHelper html)
        {
            string path = ResolveUrl(html, "~/favicon.ico");
            return string.Format("<link rel=\"shortcut icon\" href=\"{0}\" />\n", path);
        }
	}
}