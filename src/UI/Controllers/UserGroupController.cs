using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class UserGroupController : ConventionController
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupMapper _mapper;
		private readonly ISecurityContext _securityContext;

		public UserGroupController(IUserGroupRepository repository, IUserGroupMapper mapper, ISecurityContext securityContext)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
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

		[HttpGet]
		[Authorize]
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

		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult Edit(UserGroupInput input)
		{
			if (!_securityContext.HasPermissionsForUserGroup(input.Id))
			{
				return View(ViewPages.NotAuthorized);
			}

			return Command<UserGroupInput, UserGroup>(
				input,
				r => RedirectToAction<HomeController>(c => c.Index(r)),
				i => View(input));
		}

		protected bool CurrentUserHasPermissionToEditUserGroup(Guid Id)
		{
			return CurrentUserHasPermissionToEditUserGroup(_repository.GetById(Id));
		}

		protected bool CurrentUserHasPermissionToEditUserGroup(UserGroup userGroup)
		{
			return _securityContext.HasPermissionsFor(userGroup);
		}

		[Authorize]
		public ActionResult Delete(DeleteUserGroupInput input)
		{
			if (CurrentUserHasPermissionToEditUserGroup(input.UserGroup))
			{
				return NotAuthorizedView;
			}

			return Command<DeleteUserGroupInput, UserGroup>(
				input,
				r => RedirectToAction<UserGroupController>(c => c.List()),
				i => RedirectToAction<UserGroupController>(c => c.List()));
		}
	}
}