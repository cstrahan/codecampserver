using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.DependencyResolution;

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
}