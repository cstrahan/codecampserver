using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	public class UserController : SaveController<User, UserForm>
	{
		private readonly IUserMapper _mapper;
		private readonly IUserRepository _repository;
		private readonly ISecurityContext _securityContext;
		private readonly IUserSession _session;

		public UserController(IUserRepository repository, IUserMapper mapper, ISecurityContext securityContext,
		                      IUserSession session) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
			_session = session;
		}

		public ViewResult Edit(User user)
		{
			if (user == null)
			{
				return View(_mapper.Map(_session.GetCurrentUser()));
			}

			if (!_securityContext.IsAdmin())
			{
				return NotAuthorizedView;
			}
			UserForm form = _mapper.Map(user);
			return View(form);
		}

		[ValidateInput(false)]
		[ValidateModel(typeof (UserForm))]
		public ActionResult Save([Bind(Prefix = "")] UserForm form)
		{
			return ProcessSave(form, user => RedirectToAction<HomeController>(c => c.Index(null)));
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

		public ViewResult New()
		{
			return View("Edit", new UserForm());
		}

		public ViewResult Index()
		{
			return View(_mapper.Map(_repository.GetAll()));
		}
	}
}