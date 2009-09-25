using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Filters
{
	public class AdminUserCreatedFilterAttribute : ActionFilterAttribute
	{
		private readonly IUserRepository _repository;

		public AdminUserCreatedFilterAttribute(IUserRepository repository)
		{
			_repository = repository;
		}

		public AdminUserCreatedFilterAttribute() : this(DependencyRegistrar.Resolve<IUserRepository>()) {}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			int userCount = _repository.GetAll().Length;
			if (userCount == 0)
			{
				RedirectToAdminNew(filterContext);
			}
		}

		public virtual void RedirectToAdminNew(ActionExecutingContext context)
		{
			var urlhelper = new UrlHelper(context.RequestContext);
			string url = urlhelper.Action<UserController>(c => c.Edit((User)null));
			context.Result = new RedirectResult(url);
		}
	}
}