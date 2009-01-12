using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using StructureMap;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class RequireAuthenticationFilterAttribute : ActionFilterAttribute
	{
		private readonly IUserSession _session;

		public RequireAuthenticationFilterAttribute(IUserSession session)
		{
			_session = session;
		}

		public RequireAuthenticationFilterAttribute() : this(ObjectFactory.GetInstance<IUserSession>())
		{
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var controller = filterContext.Controller;
			User user = _session.GetCurrentUser();
			if (user == null)
			{
				RedirectToLogin(filterContext.HttpContext);
			}
		}

		public virtual void RedirectToLogin(HttpContextBase httpContext) {
			//use the current url for the redirect
			string redirectOnSuccess = httpContext.Request.Url.AbsolutePath;

			//send them off to the login page
			string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
			string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
			httpContext.Response.Redirect(loginUrl, true);
		}
	}
}