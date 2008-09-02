using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Presentation;
using MvcContrib;

namespace CodeCampServer.Website.Controllers
{
	public abstract class BaseController : Controller
	{
		private readonly IUserSession _userSession;

		protected BaseController(IUserSession userSession)
		{
			_userSession = userSession;
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			LogAction(filterContext);
			
			if (_userSession.IsAdministrator)			
				ViewData.Add("ShouldRenderAdminPanel", true);			

			base.OnActionExecuting(filterContext);
		}

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            PreparePageTitle();
            base.OnActionExecuted(filterContext);
        }

		private void LogAction(ControllerContext filterContext)
		{
			if (filterContext != null)
			{
				var controller = (Controller) filterContext.Controller;
				var routeData = controller.ControllerContext.RouteData;
				var actionName = routeData.Values["action"];
				var controllerName = routeData.Values["controller"];
				Log.Debug(this, string.Format("Executing action:{0} on controller:{1}", actionName, controllerName));
			}
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