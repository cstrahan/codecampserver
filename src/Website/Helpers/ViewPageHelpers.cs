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

        public static string Stylesheet(this ViewMasterPage masterPage, string cssFile)
        {
            string cssPath = cssFile.Contains("~") ? cssFile : "~/content/css/" + cssFile;

            return string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" />\n",
                                 masterPage.ResolveUrl(cssPath));
        }

        public static string ScriptInclude(this ViewMasterPage masterPage, string jsFile)
        {
            string jsPath = jsFile.Contains("~") ? jsFile : "~/content/js/" + jsFile;
            return string.Format("<script type=\"text/javascript\" src=\"{0}\" ></script>\n",
                                 masterPage.ResolveUrl(jsPath));
        }

        public static string Favicon(this ViewMasterPage masterPage)
        {
            string path = masterPage.ResolveUrl("~/favicon.ico");
            return string.Format("<link rel=\"shortcut icon\" href=\"{0}\" />\n", path);
        }
	}
}