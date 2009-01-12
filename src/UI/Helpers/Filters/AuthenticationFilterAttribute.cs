using System.Web.Mvc;
using System.Web.Security;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using StructureMap;
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

		public AuthenticationFilterAttribute() : this(ObjectFactory.GetInstance<IUserSession>())
		{
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var controller = filterContext.Controller;
			User user = _session.GetCurrentUser();
			if (user != null)
			{
				controller.ViewData.Add(user);
			}
			
		}
	}
}