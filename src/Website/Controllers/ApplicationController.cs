using System;
using System.Reflection;
using System.Web.Mvc;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;
using CodeCampServer.Model.Presentation;

namespace CodeCampServer.Website.Controllers
{
	public abstract class ApplicationController : Controller
	{	    
		private readonly IAuthorizationService _authorizationService;
		private SmartBag _smartBag;

		protected ApplicationController(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
		}

		public virtual SmartBag SmartBag
		{
			get
			{
				if (_smartBag == null)
				{
					_smartBag = new SmartBag();
				}
				return _smartBag;
			}
		}

		protected override void OnActionExecuting(FilterExecutingContext filterContext)
		{
			if (_authorizationService.IsAdministrator)
			{
				SmartBag.Add("ShouldRenderAdminPanel", true);
			}

			base.OnActionExecuting(filterContext);
		}
        
	    private void PreparePageTitle()
	    {
	        if(SmartBag.Contains<ScheduledConference>())
	        {
	            SmartBag.Add("PageTitle", SmartBag.Get<ScheduledConference>().Name);
	        }
	        else
	        {
	            SmartBag.Add("PageTitle", "Code Camp Server");
	        }
	    }

	    protected new void RenderView(string viewName)
		{
			RenderView(viewName, String.Empty, null);
		}

		protected new void RenderView(string viewName, string masterName)
		{
			RenderView(viewName, masterName, null);
		}

		protected override void RenderView(string viewName, string masterName, object viewData)
		{
			//if they passed in view data, use that. Otherwise automatically pass in our SmartBag.
			if (viewData == null)
				viewData = SmartBag;

			base.RenderView(viewName, masterName, viewData);
		}
	}    
}
