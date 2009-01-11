using System.Web.Mvc;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.UI.Helpers;

namespace CodeCampServer.UI.Controllers
{
	public class AdminController : SmartController
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserMapper _mapper;

		public AdminController(IUserRepository userRepository, IUserMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public ViewResult Edit(User user)
		{
			if (user == null)
			{
				var allUsers = _userRepository.GetAll();
				if (allUsers.Length > 0)
				{
					return Edit(allUsers[0]);
				}

				user = new User {Username = "admin"};
			}

			UserForm form = _mapper.Map(user);
			return View(form);
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

		[ValidateModel(typeof (UserForm))]
		public ActionResult Save([Bind(Prefix = "")] UserForm form)
		{
			if (!ModelState.IsValid)
			{
				return View("Edit", form);
			}

			User user = _mapper.Map(form);
			ValidationResult result = user.Validate();

			ModelState.AddModelErrors(result.GetErrors());
			if (!ModelState.IsValid)
			{
				return View("Edit", form);
			}

			_userRepository.Save(user);

			return RedirectToAction<AdminController>(c => c.Index());
		}
	}
}