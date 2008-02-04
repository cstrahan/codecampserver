using System;
using System.Web.Mvc;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public abstract class ApplicationController : Controller
    {
        private IAuthorizationService _authorizationService;


        protected ApplicationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        private SmartBag _smartBag;
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

        protected override bool OnPreAction(string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (_authorizationService.IsAdministrator)
            {
                SmartBag.Add("ShouldRenderAdminPanel", true);
            }
            
            return base.OnPreAction(actionName, methodInfo);
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
            if(viewData == null)
                viewData = SmartBag;

            base.RenderView(viewName, masterName, viewData);
        }

    }
}
