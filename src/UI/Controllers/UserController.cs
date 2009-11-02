using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;
using CommandProcessor;
using Tarantino.RulesEngine;

namespace CodeCampServer.UI.Controllers
{
	public class UserController : SmartController
	{
		private readonly IUserMapper _mapper;
		private readonly IUserRepository _repository;
		private readonly ISecurityContext _securityContext;
		private readonly IUserSession _session;
		private readonly IRulesEngine _rulesEngine;

		public UserController(IUserRepository repository, IUserMapper mapper, ISecurityContext securityContext, IUserSession session, IRulesEngine rulesEngine) 
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
			_session = session;
			_rulesEngine = rulesEngine;
		}

			[AcceptVerbs(HttpVerbs.Get)]
			public ViewResult Edit(User user)
			{
				if (!_securityContext.IsAdmin())
				{
					return NotAuthorizedView;
				}

				if (user == null)
				{
					return View(_mapper.Map(new User()));
				}

				UserInput input = _mapper.Map(user);
				return View(input);
			}

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		//[ValidateModel(typeof (UserInput))]
		public ActionResult Edit(UserInput input)
		{
			if (!_securityContext.HasPermissionsForUserGroup(input.Id))
			{
				return View(ViewPages.NotAuthorized);
			}

			if (ModelState.IsValid)
			{
				ExecutionResult result = _rulesEngine.Process(input);
				if (result.Successful)
				{
					return RedirectToAction<HomeController>(c => c.Index(null));
				}

				foreach (var errorMessage in result.Messages)
				{
					ModelState.AddModelError(errorMessage.IncorrectAttribute, errorMessage.MessageText);
				}
			}
			return View(input);
		}

		public ViewResult Index()
		{
			return View(_mapper.Map(_repository.GetAll()));
		}
	}
}