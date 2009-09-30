using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class UserController : SaveController<User, UserInput>
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

		[AcceptVerbs(HttpVerbs.Get)]
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
			UserInput input = _mapper.Map(user);
			return View(input);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateInput(false)]
		[ValidateModel(typeof (UserInput))]
		public ActionResult Edit(UserInput input)
		{
			return ProcessSave(input, user => RedirectToAction<HomeController>(c => c.Index(null)));
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(UserInput input)
		{
			var result = new ValidationResult();
			if (UsernameIsDuplicate(input))
			{
				result.AddError<UserInput>(u => u.Username, "This username already exists");
			}

			return result.GetAllErrors();
		}

		private bool UsernameIsDuplicate(UserInput input)
		{
			if (input.Id != Guid.Empty) return false;

			return _repository.GetByKey(input.Username) != null;
		}

		public ViewResult New()
		{
			return View("Edit", new UserInput());
		}

		public ViewResult Index()
		{
			return View(_mapper.Map(_repository.GetAll()));
		}
	}
}