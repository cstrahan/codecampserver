using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Controllers;
using MvcContrib;


namespace CodeCampServer.UI.Helpers.Filters
{
	public class AddUserGroupToViewDataActionFilterAttribute : ActionFilterAttribute
	{
		private readonly IUserGroupRepository _repository;

		public AddUserGroupToViewDataActionFilterAttribute() : this(DependencyRegistrar.Resolve<IUserGroupRepository>()) { }

        public AddUserGroupToViewDataActionFilterAttribute(IUserGroupRepository repository)
		{
			_repository = repository;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
            if(filterContext.RequestContext.RouteData.Values.ContainsKey("UserGroupKey"))
            {
                string key = filterContext.RequestContext.RouteData.Values["UserGroupKey"].ToString();

                var userGroup = _repository.GetByKey(key);

                if (userGroup != null)
                    filterContext.Controller.ViewData.Add(userGroup);
            }
		}

	}
}