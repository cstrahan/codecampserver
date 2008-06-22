using System.Web.Mvc;

namespace CodeCampServer.Website.Helpers
{
	public class ComponentControllerBase : ComponentController
	{
		public ComponentControllerBase()
		{
		}

		public ComponentControllerBase(ViewContext viewContext)
			: base(viewContext)
		{
		}

		public new virtual void RenderView(string viewName)
		{
			RenderView(viewName, null);
		}

		public new virtual void RenderView(string viewName, object viewData)
		{
			base.RenderView(viewName, viewData);
		}
	}
}