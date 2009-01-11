using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	public class AdminController : SaveController<User, UserForm>
	{
		private readonly IUserRepository _repository;
		private readonly IUserMapper _mapper;

		public AdminController(IUserRepository repository, IUserMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public ViewResult Edit(User user)
		{
			if (user == null)
			{
				var allUsers = _repository.GetAll();
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
			User user = _repository.GetByUserName("admin");
			if (user == null)
			{
				return RedirectToAction<AdminController>(c => c.Edit(null));
			}
			return View();
		}

		[ValidateModel(typeof (UserForm))]
		public ActionResult Save([Bind(Prefix = "")] UserForm form)
		{
			return ProcessSave(form, () => RedirectToAction<AdminController>(c => c.Index()));
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(UserForm form)
		{
			var result = new ValidationResult();
			if (UsernameIsDuplicate(form))
			{
				result.AddError<UserForm>(u => u.Username, "This username already exists");
			}

			return result.GetAllErrors();
		}

		private bool UsernameIsDuplicate(UserForm form)
		{
			if (form.Id != Guid.Empty) return false;

			return _repository.GetByKey(form.Username) != null;
		}
	}
}