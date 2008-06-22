using System.Web.Mvc;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
	public class RequireLoginAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				base.OnActionExecuting(filterContext);
			}
			else
			{
				filterContext.Cancel = true;
				filterContext.Result = RedirectHelper.RedirectToLogin(filterContext.HttpContext.Request);
			}
		}
	}
}