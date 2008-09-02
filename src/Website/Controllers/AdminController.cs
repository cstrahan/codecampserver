using System.Web.Mvc;
using CodeCampServer.Model;

namespace CodeCampServer.Website.Controllers
{
	[Authorize(Roles="Administrator")]
	public class AdminController : BaseController
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