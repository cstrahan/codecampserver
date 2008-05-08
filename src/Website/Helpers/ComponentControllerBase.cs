using System.Web.Mvc;

namespace CodeCampServer.Website.Helpers
{
    public class ComponentControllerBase : ComponentController
    {
        public ComponentControllerBase()
            : base()
        {
        }

        public ComponentControllerBase(ViewContext viewContext)
            : base(viewContext)
        {
        }

        public virtual new void RenderView(string viewName)
        {
            RenderView(viewName, null);
        }

        public virtual new void RenderView(string viewName, object viewData)
        {
            base.RenderView(viewName, viewData);
        }
    }
}