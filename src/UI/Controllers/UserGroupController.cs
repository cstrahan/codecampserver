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
	public class UserGroupController : SmartController
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupMapper _mapper;
		private readonly ISecurityContext _securityContext;
		private readonly IRulesEngine _rulesEngine;

		public UserGroupController(IUserGroupRepository repository, IUserGroupMapper mapper, ISecurityContext securityContext, IRulesEngine rulesEngine) 
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
			_rulesEngine = rulesEngine;
		}


		public ActionResult Index(UserGroup usergroup)
		{
			return View(_mapper.Map(usergroup));
		}

		public ActionResult List()
		{
			UserGroup[] entities = _repository.GetAll();
			return View(_mapper.Map(entities));
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[RequireAuthenticationFilter]
		public ActionResult Edit(Guid? entityToEdit)
		{
			UserGroup model;
			if (!entityToEdit.HasValue || entityToEdit == Guid.Empty)
			{
				model = new UserGroup();
			}
			else
			{
				model = _repository.GetById(entityToEdit.Value);
				if (!CurrentUserHasPermissionToEditUserGroup(model))
				{
					return View(ViewPages.NotAuthorized);
				}
			}
			return View(_mapper.Map(model));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		//[ValidateModel(typeof (UserGroupInput))]
		public ActionResult Edit(UserGroupInput input)
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
					var userGroup = result.ReturnItems.Get<UserGroup>();
					return RedirectToAction<HomeController>(c => c.Index(userGroup));
				}
				
				foreach (var errorMessage in result.Messages)
				{
					ModelState.AddModelError(errorMessage.IncorrectAttribute,errorMessage.MessageText);					
				}
			}
			return View(input);
		}

		protected bool CurrentUserHasPermissionToEditUserGroup(Guid Id)
		{
			return CurrentUserHasPermissionToEditUserGroup(_repository.GetById(Id));
		}

		protected bool CurrentUserHasPermissionToEditUserGroup(UserGroup userGroup)
		{
			return _securityContext.HasPermissionsFor(userGroup);
		}

		[RequireAdminAuthorizationFilter]
		public ActionResult Delete(DeleteUserGroupInput input)
		{
			if (CurrentUserHasPermissionToEditUserGroup(input.UserGroup))
			{
				return NotAuthorizedView;
			}

			var result = _rulesEngine.Process(input);

			if (result.Successful)
			{
				TempData.Add("message", result.ReturnItems.Get<UserGroup>().Name + " was deleted.");
			}
			else
			{
				TempData.Add("message", result.Messages[0]);
			}
			return RedirectToAction<UserGroupController>(c => c.List());
		}
	}
}