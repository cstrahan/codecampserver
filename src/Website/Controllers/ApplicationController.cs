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

		protected ApplicationController(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
		}
        
		protected override void OnActionExecuting(FilterExecutingContext filterContext)
		{            
			if (_authorizationService.IsAdministrator)
			{
				ViewData.Add("ShouldRenderAdminPanel", true);
			}
            PreparePageTitle();

			base.OnActionExecuting(filterContext);
		}
        
	    private void PreparePageTitle()
	    {
            if (ViewData.Contains<ScheduledConference>())
            {
                string conferenceName = ViewData.Get<ScheduledConference>().Name;
                ViewData.Add("PageTitle", conferenceName);
            }
            else
	        {
                ViewData.Add("PageTitle", "Code Camp Server");
	        }
	    }
	}    
}
