using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Helpers
{
    public static class ConferenceViewExtensions
    {
        public static string ConferenceLink(this HtmlHelper html, ConferenceInput conferenceInput)
        {            
            return html.RouteLink(conferenceInput.Name, "conferencedefault", 
                new { controller="conference", action="index", conferenceKey = conferenceInput.Key }).ToString();
        }

        private static string GetDateBadge(DateTime dtm)
        {
            return string.Format(@"
                <div class='datebadge'>
                <span class='month'>{0}</span>
                <span class='day'>{1}</span>
                <span class='year'>{2}</span>",
            dtm.Month, dtm.Day, dtm.Year);
        }

        public static string DateBadge(this HtmlHelper html, ConferenceInput conferenceInput)
        {
            return "";
        }
    }
}
