using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using CodeCampServer.Website.Views;
using CodeCampServer.Model.Security;

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
                this.SmartBag.Add("ShouldRenderAdminPanel", true);
            }
            
            return base.OnPreAction(actionName, methodInfo);
        }

        protected override void RenderView(string viewName, string masterName, object viewData)
        {
            //if they passed in view data, use that. Otherwise automatically pass in our SmartBag.
            if(viewData == null)
                viewData = this.SmartBag;

            base.RenderView(viewName, masterName, viewData);
        }
    }
}
