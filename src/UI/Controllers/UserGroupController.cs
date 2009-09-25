using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;
using CodeCampServer.Core.Services.Impl;
using MvcContrib;
using CodeCampServer.UI;
using CodeCampServer.Core.Services;


namespace CodeCampServer.UI.Controllers
{
	public class UserGroupController : SaveController<UserGroup, UserGroupForm>
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupMapper _mapper;
        private readonly ISecurityContext _securityContext;

	    public UserGroupController(IUserGroupRepository repository, IUserGroupMapper mapper,IConferenceRepository conferenceRepository,IConferenceMapper conferenceMapper, ISecurityContext securityContext) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
	        _securityContext = securityContext;
		}

		
		public ActionResult Index(UserGroup usergroup)
		{

            UserGroupForm form = _mapper.Map(usergroup);
			return View(form);
		}

		public ActionResult List()
		{
			UserGroup[] entities = _repository.GetAll();

			if (entities.Length < 1)
			{
				return RedirectToAction<UserGroupController>(c => c.New());
			}
			object entityListDto = AutoMapper.Mapper .Map(entities, typeof (UserGroup[]), typeof (UserGroupForm[]));
			return View(entityListDto);
		}
		
        [RequireAuthenticationFilter]
		public ActionResult Edit(Guid Id)
		{

			if (Id == Guid.Empty)
			{
				TempData.Add("message", "UserGroup has been deleted.");
				return RedirectToAction<UserGroupController>(c => c.List());
			}

            UserGroup userGroup = _repository.GetById(Id);
            if(!CurrentUserHasPermissionToEditUserGroup(userGroup))
            {
                return View(ViewPages.NotAuthorized);
            }
            return View(_mapper.Map(userGroup));
		}

	    protected bool CurrentUserHasPermissionToEditUserGroup(Guid Id)
	    {
	        return CurrentUserHasPermissionToEditUserGroup(_repository.GetById(Id));
	    }

        protected bool CurrentUserHasPermissionToEditUserGroup(UserGroup userGroup)
        {
            return _securityContext.HasPermissionsFor(userGroup);
        }

	    [ValidateInput(false)] 
		[ValidateModel(typeof (UserGroupForm))]
		public ActionResult Save(UserGroupForm form)
		{
            if(_securityContext.HasPermissionsForUserGroup(form.Id))
            {
                return ProcessSave(form, entity => RedirectToAction<UserGroupController>(c => c.List()));
            }
	        return View(ViewPages.NotAuthorized);
		}
        

		protected override IDictionary<string, string[]> GetFormValidationErrors(UserGroupForm form)
		{
			var result = new ValidationResult();
			if (CurrentUserHasPermissionToEditUserGroup(form.Id) && UserGroupKeyAlreadyExists(form))
			{
				result.AddError<UserGroupForm>(x => x.Key, "This entity key already exists");
			}
			return result.GetAllErrors();
		}

	    private bool UserGroupKeyAlreadyExists(UserGroupForm message)
		{
			UserGroup entity = _repository.GetByKey(message.Key);
			return entity != null && entity.Id != message.Id;
		}

		
        [RequireAdminAuthorizationFilter]
		public ActionResult New()
		{
			return View("Edit", _mapper.Map(new UserGroup()));
		}

        [RequireAdminAuthorizationFilter]
        public ActionResult Delete(UserGroup entity)
        {
            if (!CurrentUserHasPermissionToEditUserGroup(entity))
            {
                return View(ViewPages.NotAuthorized);
            }

            if (entity.GetUsers().Length == 0)
            {
                _repository.Delete(entity);
            }
            else
            {
                TempData.Add("message", "UserGroup cannot be deleted.");
            }

            return RedirectToAction<UserGroupController>(c => c.List());
        }

	}
}



