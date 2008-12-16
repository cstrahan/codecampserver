using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.UI.Controllers;

namespace CodeCampServer.UI
{
    public abstract class SubController : SmartController, ISubController
    {
        #region ISubController Members

        public virtual Action GetResult(ControllerBase parentController)
        {
            Merge(parentController.TempData);
            RequestContext requestContext = GetNewRequestContextFromController(parentController);
            return () => Execute(requestContext);
        }

        #endregion

        private void Merge(TempDataDictionary dictionary)
        {
            foreach (var entry in dictionary)
            {
                TempData[entry.Key] = entry.Value;
            }
        }

        protected override void ExecuteCore()
        {
            // subcontrollers always need the parent's tempdata, base.ExecuteCore() removes it
            string actionName = RouteData.GetRequiredString("action");
            if (!ActionInvoker.InvokeAction(ControllerContext, actionName))
            {
                HandleUnknownAction(actionName);
            }
        }

        public RequestContext GetNewRequestContextFromController(ControllerBase parentController)
        {
            RouteData parentRouteData = parentController.ControllerContext.RouteData;
            var routeData = new RouteData(parentRouteData.Route, parentRouteData.RouteHandler);
            string controllerName = GetControllerName();
            routeData.Values["controller"] = controllerName;
            routeData.Values["action"] = controllerName;
            HttpContextBase context = parentController.ControllerContext.HttpContext;
            return new RequestContext(context, routeData);
        }

        public string GetControllerName()
        {
            return GetType().Name.ToLowerInvariant().Replace("subcontroller", "").Replace("controller", "");
        }
    }

    public interface ISubController<T> : ISubController
    {
        T Model { get; set; }
    }

    public abstract class SubController<T> : SubController, ISubController<T>
    {
        #region ISubController<T> Members

        public virtual T Model { get; set; }

        #endregion
    }
}