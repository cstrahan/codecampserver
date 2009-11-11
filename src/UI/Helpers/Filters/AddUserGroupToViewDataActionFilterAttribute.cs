using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Filters;
using MvcContrib;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AddUserGroupToViewDataActionFilterAttribute : ContainerBaseActionFilter, IConventionActionFilter
	{
		private readonly IUserGroupRepository _repository;

		public AddUserGroupToViewDataActionFilterAttribute() 
		{
			_repository = CreateDependency<IUserGroupRepository>();
		}

		public AddUserGroupToViewDataActionFilterAttribute(IUserGroupRepository repository)
		{
			_repository = repository;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string userGroupValue = GetUserGroupValue(filterContext);

			if (userGroupValue != null)
			{
				UserGroup userGroup = _repository.GetByKey(userGroupValue);

				if (userGroup != null)
					filterContext.Controller.ViewData.Add(userGroup);
			}
		}

		private string GetUserGroupValue(ActionExecutingContext filterContext)
		{
			string userGroupValue = null;
			string usergroupkey = "UserGroupKey";

			if (filterContext.RequestContext.RouteData.Values.ContainsKey(usergroupkey))
			{
				userGroupValue = filterContext.RequestContext.RouteData.Values[usergroupkey].ToString();
			}
			return userGroupValue;
		}
	}
}