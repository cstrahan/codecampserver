using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Filters;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AddUserToViewDataAttribute : ContainerBaseActionFilter,IConventionActionFilter
	{
		private readonly IUserSession _session;

		public AddUserToViewDataAttribute(IUserSession session)
		{
			_session = session;
		}

		public AddUserToViewDataAttribute()
		{
			_session= CreateDependency<IUserSession>();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			ControllerBase controller = filterContext.Controller;
			User user=null;
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