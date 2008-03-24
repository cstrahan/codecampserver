using System.Web.Mvc;

namespace CodeCampServer.UnitTests.Website
{
    public class ViewEngineStub : IViewEngine
    {
        public ViewContext ActualViewContext;

        public void RenderView(ViewContext viewContext)
        {
            ActualViewContext = viewContext;
        }
    }
}
