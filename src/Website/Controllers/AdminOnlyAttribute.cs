using System.Web.Mvc;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
	public class AdminOnlyAttribute : ActionFilterAttribute
	{
		private readonly IAuthorizationService _authorizationService;

		public AdminOnlyAttribute() : this(IoC.Resolve<IAuthorizationService>())
		{
		}

		public AdminOnlyAttribute(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (_authorizationService.IsAdministrator)
			{
				base.OnActionExecuting(filterContext);
			}
			else
			{
				//cancel this request & redirect to login page
				filterContext.Cancel = true;
				filterContext.Result = RedirectHelper.RedirectToLogin(filterContext.HttpContext.Request);
			}
		}
	}
}