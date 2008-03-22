using System.Web.Mvc;

namespace CodeCampServer.Website.Controllers
{
    public class SponsorComponentController : ComponentController
    {
        public void Index()
        {
            RenderView("Sponsors");
        }
    }
}