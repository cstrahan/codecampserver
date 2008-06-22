using System.Web.Mvc;

namespace CodeCampServer.Website.Controllers
{
	/// <summary>
	/// Redirects the user to the raw url specified.
	/// </summary>
	public class UrlRedirectResult : ActionResult
	{
		public string Url { get; private set; }

		public UrlRedirectResult(string url)
		{
			Url = url;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.Redirect(Url);
		}
	}
}