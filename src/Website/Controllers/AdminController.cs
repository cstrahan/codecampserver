using System.Web.Mvc;
using CodeCampServer.Model;

namespace CodeCampServer.Website.Controllers
{
	[AdminOnly]
	public class AdminController : Controller
	{
		public AdminController(IUserSession userSession)
			: base(userSession)
		{
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Schedule()
		{
			return View();
		}
	}
}