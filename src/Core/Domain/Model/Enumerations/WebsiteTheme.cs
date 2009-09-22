

namespace CodeCampServer.Core.Domain.Model.Enumerations
{
	public class WebsiteTheme : Enumeration
	{
		public static WebsiteTheme Green = new WebsiteTheme(1, "Green");
		public static WebsiteTheme Emporium = new WebsiteTheme(2, "Emporium");

		public WebsiteTheme() {}
		private WebsiteTheme(int value, string displayName) : base(value, displayName) {}
	}
}