using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
	public class AdminOnlyAttribute : ActionFilterAttribute
	{
		private readonly IUserSession _userSession;

		public AdminOnlyAttribute() : this(IoC.Resolve<IUserSession>())
		{
		}

		public AdminOnlyAttribute(IUserSession userSession)
		{
			_userSession = userSession;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!_userSession.IsAdministrator)
			{
				//cancel this request & redirect to login page
				filterContext.Cancel = true;
				filterContext.Result = RedirectHelper.RedirectToLogin(filterContext.HttpContext.Request);
			}
			else
			{
				base.OnActionExecuting(filterContext);
			}
		}
	}
}