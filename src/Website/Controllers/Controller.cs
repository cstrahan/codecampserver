using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Model;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
	public abstract class Controller : System.Web.Mvc.Controller
	{
		private readonly IUserSession _userSession;

		protected Controller(IUserSession userSession)
		{
			_userSession = userSession;
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext != null)
			{
				var controller = (System.Web.Mvc.Controller) filterContext.Controller;
				RouteData routeData = controller.ControllerContext.RouteData;
				object actionName = routeData.Values["action"];
				object controllerName = routeData.Values["controller"];
				Log.Debug(this, string.Format("Executing action:{0} on controller:{1}", actionName, controllerName));
			}
			
			if (_userSession.IsAdministrator)
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