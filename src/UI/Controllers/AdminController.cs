using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class AdminController : ConventionController
	{
		private readonly IUserRepository _repository;

		public AdminController(IUserRepository repository)
		{
			_repository = repository;
		}


		public ActionResult Index(Conference conference)
		{
			User user = _repository.GetByUserName("admin");
			if (user == null)
			{
				return RedirectToAction<UserController>(c => c.Edit((User) null));
			}
			var model = new AdminInput {ConferenceIsSelected = conference != null};

			return View(model);
		}
	}
}