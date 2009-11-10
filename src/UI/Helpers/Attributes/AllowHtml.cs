using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.Attributes
{
	public class AllowHtml:ValidateInputAttribute
	{
		public AllowHtml() : base(false) {}
	}
}