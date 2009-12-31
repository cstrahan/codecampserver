using System.Web;
using System.Web.Mvc;
using CodeCampServer.UI.Services;

namespace CodeCampServer.UI.Helpers.Extensions
{
	public static class ButtonExtensions
	{
		public static string EditImageButton(this HtmlHelper helper, string url)
		{
			return ImageButton(helper, url, "Edit", "/images/icons/application_edit.png");
		}	

		public static string AddImageButton(this HtmlHelper helper, string url)
		{
			return ImageButton(helper, url, "Add", "/images/icons/application_add.png");
		}	

		public static string ImageButton(this HtmlHelper helper, string url, string altText, string imageFile)
		{
			if (!HttpContext.Current.User.Identity.IsAuthenticated)
				return string.Empty;

			IReturnUrlManager manager = ReturnUrlManagerFactory.GetDefault();

			var targetUrl = manager.GetTargetUrlWithReturnUrl(url);
			return string.Format("<a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\" /></a>", targetUrl, imageFile, altText);
		}	
	}
}