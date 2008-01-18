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
        /// <param name="page"></param>
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
    }
}
