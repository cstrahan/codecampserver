using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;

namespace CodeCampServer.UI.Helpers
{
    public static class ImageLinkHelper
    {
        public static string ImageLink<TController>(this HtmlHelper html, Expression<Func<TController, object>> expr, object routeValues, string relativeImageUrl, string imgAlt)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var url = urlHelper.Action(expr, routeValues);
            var imageTag = html.Image(relativeImageUrl, imgAlt);
            return string.Format("<a href='{0}'>{1}</a>", url, imageTag);   
        }

        public static string ImageLink<TController>(this HtmlHelper html, Expression<Func<TController, object>> expr, string relativeImageUrl, string imgAlt)
        {
            return html.ImageLink(expr, null, relativeImageUrl, imgAlt);
        }

        public static string ImageLink(this HtmlHelper html, string relativeImageUrl, string imgAlt, string action, string controller)
        {
            return html.ImageLink(relativeImageUrl, imgAlt, action, controller, null);
        }

        public static string ImageLink(this HtmlHelper html, string relativeImageUrl, string imgAlt, string action, string controller, object routevalues)
        {
            return html.ImageLink(relativeImageUrl, imgAlt, action, controller, routevalues, null);
        }

        public static string ImageLink(this HtmlHelper html, string relativeImageUrl, string imgAlt, string action, string controller, object routevalues, object htmlAttributes)
        {
            var builder = new TagBuilder("a");
            var url = new UrlHelper(html.ViewContext.RequestContext);

            builder.Attributes["href"] = routevalues != null
                                             ? url.Action(action, controller, routevalues)
                                             : url.Action(action, controller);

            
            if(htmlAttributes != null)
            {
                var attributeDictionary = new RouteValueDictionary(htmlAttributes);
                foreach (var key in attributeDictionary.Keys)
                {
                    builder.Attributes[key] = attributeDictionary[key].ToString();
                }
            }

            builder.InnerHtml = html.Image(relativeImageUrl, imgAlt);

            return builder.ToString(TagRenderMode.Normal);
        }        
    }
}