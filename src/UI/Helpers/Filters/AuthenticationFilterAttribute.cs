using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.DependencyResolution;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AuthenticationFilterAttribute : ActionFilterAttribute
	{
		private readonly IUserSession _session;

		public AuthenticationFilterAttribute(IUserSession session)
		{
			_session = session;
		}

		public AuthenticationFilterAttribute()
			: this(DependencyRegistrar.Resolve<IUserSession>()) {}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			ControllerBase controller = filterContext.Controller;
			User user = _session.GetCurrentUser();
			if (user != null)
			{
				controller.ViewData.Add(user);
			}
		}
	}
}