using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
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
			RouteValueDictionary values = filterContext.RouteData.Values;
			var key =
				(string) (values["conferenceKey"] ?? values["conference"]);
			ViewDataDictionary viewData = filterContext.Controller.ViewData;
			if(string.IsNullOrEmpty(key))
			{
				Conference conf = _repository.GetNextConference();
				viewData.Add(conf);
				return;
			}

			var conference = _repository.GetByKey(key);
			if (conference == null)
			{
				Expression<Func<ConferenceController, object>> actionExpression = c => c.New();

				string controllerName = typeof (ConferenceController).GetControllerName();
				string actionName = actionExpression.GetActionName();

				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new{controller = controllerName, action = actionName}));
				return;
			}

			viewData.Add(conference);
		}
	}
}