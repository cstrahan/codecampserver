using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;


namespace CodeCampServer.UI.Controllers
{
	public class UserGroupController : SaveController<UserGroup, UserGroupForm>
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupMapper _mapper;

		public UserGroupController(IUserGroupRepository repository, IUserGroupMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
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
		
        [RequireAdminAuthorizationFilter]
		public ActionResult Edit(UserGroup usergroup)
		{
			if (usergroup == null)
			{
				TempData.Add("message", "UserGroup has been deleted.");
				return RedirectToAction<UserGroupController>(c => c.List());
			}
			return View(_mapper.Map(usergroup));
		}

        
        [RequireAdminAuthorizationFilter]
        [ValidateInput(false)] 
		[ValidateModel(typeof (UserGroupForm))]
		public ActionResult Save([Bind(Prefix = "")] UserGroupForm form)
		{
			return ProcessSave(form, entity => RedirectToAction<UserGroupController>(c => c.List()));
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(UserGroupForm form)
		{
			var result = new ValidationResult();
			if (UserGroupKeyAlreadyExists(form))
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



