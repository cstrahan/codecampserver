using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
	// feel free to rename this if you have a better convention
	public class FlashMessageComponentController : Controller
	{
		private readonly IUserSession _session;

		public FlashMessageComponentController(IUserSession session) : base(session)
		{
			_session = session;
		}

		public ActionResult GetMessages()
		{
			FlashMessage[] flashMessages = _session.PopUserMessages();
            return View("FlashMessageList", flashMessages);
		}
	}
}