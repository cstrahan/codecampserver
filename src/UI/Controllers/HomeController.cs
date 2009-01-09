using System.Web.Mvc;
using CodeCampServer.UI.Helpers.Filters;

namespace CodeCampServer.UI.Controllers
{
	[RequiresConferenceFilter]
	public class HomeController : SmartController
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}