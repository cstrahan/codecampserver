using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Filters;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	public class AdminController : SaveController<User, IUserMessage>
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserUpdater _updater;

		public AdminController(IUserRepository userRepository, IUserUpdater updater)
		{
			_userRepository = userRepository;
			_updater = updater;
		}

		[AutoMappedToModelFilter(typeof (User), typeof (UserForm))]
		public ViewResult Edit(User user)
		{
			if (user == null)
			{
				var allUsers = _userRepository.GetAll();
				if(allUsers.Length > 0)
				{
					return Edit(allUsers[0]);
				}

				user = new User {Username = "admin"};
			}

			ViewData.Add(user);
			return View();
		}

		public ActionResult Index()
		{
			User user = _userRepository.GetByUserName("admin");
			if (user == null)
			{
				return RedirectToAction<AdminController>(c => c.Edit(null));
			}
			return View();
		}

		[ValidateModel(typeof(UserForm))]
		public ActionResult Save([Bind(Prefix = "")] UserForm form)
		{
			return ProcessSave(form, () => RedirectToAction<AdminController>(c => c.Index()));
		}

		protected override IModelUpdater<User, IUserMessage> GetUpdater()
		{
			return _updater;
		}
	}
}