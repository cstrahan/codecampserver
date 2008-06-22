using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace CodeCampServer.Website.Controllers
{
	public class AuthorizationFilterAttribute : ActionFilterAttribute
	{
		public string AllowRoles { get; set; }

		private bool HasAtLeastOneRole(IPrincipal user)
		{
			foreach (string role in AllowRoles.Split(','))
			{
				if (user.IsInRole(role))
					return true;
			}

			return false;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Cancel = true;
				//no way to access RedirectToAction() or Url.Action() here....
				filterContext.HttpContext.Response.Redirect("~/login?redirectUrl=" +
				                                            HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.PathAndQuery));
			}

			if (!HasAtLeastOneRole(filterContext.HttpContext.User))
			{
				filterContext.Cancel = true;
				filterContext.HttpContext.Response.Write("You do not have access to this area.");
			}
		}
	}
}