using System.Web.Mvc;

namespace CodeCampServer.UI.Controllers
{
    public class HomeController:SmartController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}