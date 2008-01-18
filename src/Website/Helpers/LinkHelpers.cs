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
using System.Web.Extensions;
using System.Collections;

namespace CodeCampServer.Website.Helpers
{
    public static class LinkHelpers
    {
        public static bool IsActive(this ViewPage thePage, string controller, string action)
        {
            return thePage.ViewContext.RouteData.Values["controller"].ToString() == controller &&
                   thePage.ViewContext.RouteData.Values["action"].ToString() == action;            
        }
        
        public static bool IsActive(this ViewMasterPage thePage, string controller, string action)
        {
            return thePage.ViewContext.RouteData.Values["controller"].ToString() == controller &&
                   thePage.ViewContext.RouteData.Values["action"].ToString() == action;            
        }

        private static bool IsActive(this ViewPage thePage, object values)
        {
            Hashtable props = HtmlExtensionUtility.GetPropertyHash(values);

            string controller = props["controller"] as string ?? props["Controller"] as string;
            string action = props["action"] as string ?? props["Action"] as string;

            return IsActive(thePage, controller, action);
        }

        private static bool IsActive(this ViewMasterPage thePage, object values)
        {
            Hashtable props = HtmlExtensionUtility.GetPropertyHash(values);

            string controller = props["controller"] as string ?? props["Controller"] as string;
            string action = props["action"] as string ?? props["Action"] as string;

            return IsActive(thePage, controller, action);
        }

        /// <summary>
        /// Creates a link that can set a custom class if it is the current active link
        /// </summary>
        /// <param name="page">the automatic page parameter</param>
        /// <param name="text">the text of the link</param>
        /// <param name="controller">the controller</param>
        /// <param name="action">the action to call</param>
        /// <param name="classIfActive">the css class to set if this is an active link</param>
        /// <returns>an html A tag</returns>
        public static string NavLink(this ViewPage page, string text, string controller, string action, string classIfActive)
        {            
            string url = page.Url.Action(action, controller);
            bool active = IsActive(page, controller, action);
            return RenderLink(text, url, active, classIfActive);
        }        
        
        /// <summary>
        /// Creates a link that can set a custom class if it is the current active link
        /// </summary>
        /// <param name="page">the automatic page parameter</param>
        /// <param name="text">the text of the link</param>
        /// <param name="controller">the controller</param>
        /// <param name="action">the action to call</param>
        /// <param name="classIfActive">the css class to set if this is an active link</param>
        /// <returns>an html A tag</returns>
        public static string NavLink(this ViewMasterPage masterPage, string text, string controller, string action, string classIfActive)
        {                        
            string url = masterPage.Url.Action(action, controller);
            bool active = IsActive(masterPage, controller, action);
            return RenderLink(text, url, active, classIfActive);
        }
     

        public static string NavLink(this ViewMasterPage masterPage, string text, string classIfActive, object values)
        {
            string url = masterPage.Url.Action(values);
            bool active = IsActive(masterPage, values);
            return RenderLink(text, url, active, classIfActive);
        }

        private static string RenderLink(string text, string url, bool active, string classIfActive)
        {
            return string.Format("<a href='{0}' {1}>{2}</a>", url, active? "class='" + classIfActive + "'" : string.Empty, text);
        }
       
    }
}
