using System.Web.Mvc;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
	public abstract class Controller : System.Web.Mvc.Controller
	{
		private readonly IAuthorizationService _authorizationService;

		protected Controller(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (_authorizationService.IsAdministrator)
			{
				ViewData.Add("ShouldRenderAdminPanel", true);
			}
			PreparePageTitle();

			base.OnActionExecuting(filterContext);
		}

		private void PreparePageTitle()
		{
			if (ViewData.Contains<Schedule>())
			{
				string conferenceName = ViewData.Get<Schedule>().Name;
				ViewData.Add("PageTitle", conferenceName);
			}
			else
			{
				ViewData.Add("PageTitle", "Code Camp Server");
			}
		}
	}
}