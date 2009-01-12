using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain;
using CodeCampServer.UI.Controllers;
using StructureMap;
using MvcContrib;

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
				(string) (filterContext.RouteData.Values["conferenceKey"] ?? filterContext.RouteData.Values["conference"]);
			var conference = _repository.GetByKey(conferenceKey);
			if (conference == null)
			{
				Expression<Func<ConferenceController, object>> actionExpression = c => c.New();

				string controllerName = typeof (ConferenceController).GetControllerName();
				string actionName = actionExpression.GetActionName();

				filterContext.Result =
					new RedirectToRouteResult(new RouteValueDictionary(new {controller = controllerName, action = actionName}));
			}

			filterContext.Controller.ViewData.Add(conference);
		}
	}
}