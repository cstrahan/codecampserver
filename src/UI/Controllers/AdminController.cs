using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Filters;
using CodeCampServer.UI.Models.AutoMap;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	public class AdminController : SmartController
	{
		private readonly IUserRepository _userRepository;

		public AdminController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public ActionResult EditAdminPassword()
		{
			User user = _userRepository.GetByUserName("admin");
			if (user == null)
			{
				user = new User {Username = "admin"};
				_userRepository.Save(user);
			}

			object form = AutoMapper.Map(user, typeof (User), typeof (UserForm));

			return View(form);
		}

		public ActionResult Index()
		{
			User user = _userRepository.GetByUserName("admin");
			if (user == null)
			{
				return RedirectToAction<AdminController>(c => c.EditAdminPassword());
			}
			return View();
		}

		[ValidateModel(typeof (UserForm))]
		public ActionResult Save([Bind(Prefix = "")] UserForm form)
		{
			if (ModelState.IsValid)
			{
				User user = _userRepository.GetById(form.Id);
				if (user != null)
				{
					user.HashedPassword = form.Password;
					user.EmailAddress = form.EmailAddress;
					user.Name = form.Name;
					_userRepository.Save(user);
					return RedirectToAction<AdminController>(c => c.Index());
				}
			}

			return View("EditAdminPassword");
		}
	}
}