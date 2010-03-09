using System.Web.Mvc;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Services;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AddUserToViewDataAttribute : ContainerBaseActionFilter, IConventionActionFilter
	{
		private readonly IUserSession _session;

		public AddUserToViewDataAttribute(IUserSession session)
		{
			_session = session;
		}

		public AddUserToViewDataAttribute()
		{
			_session = CreateDependency<IUserSession>();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var controller = filterContext.Controller;
			User user = null;
			try
			{
				user = _session.GetCurrentUser();
			}
			catch {}

			if (user != null)
			{
				controller.ViewData.Add(user);
			}
		}
	}
}