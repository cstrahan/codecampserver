using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.DependencyResolution;

using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
    public class RequireAdminAuthorizationFilterAttribute:RequireAuthenticationFilterAttribute
    {
        private readonly ISecurityContext _securityContext;

        public RequireAdminAuthorizationFilterAttribute(IUserSession session,ISecurityContext securityContext) : base(session)
        {
            _securityContext = securityContext;
        }

        public RequireAdminAuthorizationFilterAttribute() : this(DependencyRegistrar.Resolve<IUserSession>(),DependencyRegistrar.Resolve<ISecurityContext>()) { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = _session.GetCurrentUser();
            if (user == null || !_securityContext.IsAdmin() )
            {
                RedirectToLogin(filterContext.HttpContext);
            }
        }
    }

	public class RequireAuthenticationFilterAttribute : ActionFilterAttribute
	{
		protected readonly IUserSession _session;

		public RequireAuthenticationFilterAttribute(IUserSession session)
		{
			_session = session;
		}

		public RequireAuthenticationFilterAttribute()
			: this(DependencyRegistrar.Resolve<IUserSession>())
		{
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
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