using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Controllers;
using MvcContrib;
using StructureMap;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class RequiresConferenceFilterAttribute : ActionFilterAttribute
	{
		private readonly IConferenceRepository _repository;

		public RequiresConferenceFilterAttribute() : this(ObjectFactory.GetInstance<IConferenceRepository>()) {}

		public RequiresConferenceFilterAttribute(IConferenceRepository repository)
		{
			
			_repository = repository;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Conference conference = GetConferenceFromKeyOrGetTheNextOneFromTheRepository(filterContext);

			if (conference == null)
			{
				filterContext.Result = RedirectToNewTheConferenceAction(filterContext);
			}
			else
			{
				AddConferenceToViewData(filterContext, conference);
			}
		}

		private void AddConferenceToViewData(ActionExecutingContext filterContext, Conference conference)
		{
			filterContext.Controller.ViewData.Add(conference);
		}

		private Conference GetConferenceFromKeyOrGetTheNextOneFromTheRepository(ActionExecutingContext filterContext)
		{
			string key = GetConferenceKeyValue(filterContext);

			Conference conference;

			conference = _repository.GetByKey(key);

			if (string.IsNullOrEmpty(key) || conference == null)
			{
				conference = _repository.GetNextConference();
			}

			return conference;
		}

		private RedirectToRouteResult RedirectToNewTheConferenceAction(ActionExecutingContext filterContext)
		{
			Expression<Func<ConferenceController, object>> actionExpression = c => c.New();

			string controllerName = typeof (ConferenceController).GetControllerName();
			string actionName = actionExpression.GetActionName();

			return new RedirectToRouteResult(new RouteValueDictionary(new {controller = controllerName, action = actionName}));
		}

		private string GetConferenceKeyValue(ActionExecutingContext filterContext)
		{
			RouteValueDictionary values = filterContext.RouteData.Values;
			return (string) (values["conferenceKey"] ?? values["conference"]);
		}
	}
}