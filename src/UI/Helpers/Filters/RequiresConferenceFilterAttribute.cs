using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain;
using CodeCampServer.UI.Controllers;
using StructureMap;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class RequiresConferenceFilterAttribute : ActionFilterAttribute
	{
		private readonly IConferenceRepository _repository;

		public RequiresConferenceFilterAttribute() : this(ObjectFactory.GetInstance<IConferenceRepository>())
		{
		}

		public RequiresConferenceFilterAttribute(IConferenceRepository repository)
		{
			_repository = repository;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var conferenceKey =
				(string) filterContext.RouteData.Values["conferenceKey"];
			var conference = _repository.GetByKey(conferenceKey);
			if (conference == null)
			{
				Expression<Func<AdminController, object>> actionExpression =
					c => c.Edit(null);

				string controllerName = typeof (AdminController).GetControllerName();
				string actionName = actionExpression.GetActionName();

				filterContext.Result =
					new RedirectToRouteResult(
						new RouteValueDictionary(
							new {controller = controllerName, action = actionName}));
			}
		}
	}
}