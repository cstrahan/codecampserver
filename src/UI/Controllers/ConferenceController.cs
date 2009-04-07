using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models;
using CodeCampServer.UI.Models.Forms;
using MvcContrib;

namespace CodeCampServer.UI.Controllers
{
	[AdminUserCreatedFilter]
	public class ConferenceController : SaveController<Conference, ConferenceForm>
	{
		private readonly IConferenceRepository _repository;
		private readonly IConferenceMapper _mapper;
	    private readonly ISecurityContext _securityContext;
	    private readonly IUserGroupRepository _userGroupRepository;

	    public ConferenceController(IConferenceRepository repository, IConferenceMapper mapper,ISecurityContext securityContext,IUserGroupRepository userGroupRepository) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
		    _securityContext = securityContext;
	        _userGroupRepository = userGroupRepository;
		}

		[RequiresConferenceFilter]
		public ActionResult Index(Conference conference)
		{
			ConferenceForm form = _mapper.Map(conference);
			return View(form);
		}

		public ActionResult List(UserGroup usergroup)
		{
			//ViewData.Add(new PageInfo {Title = usergroup.Name});

			Conference[] conferences = _repository.GetAllForUserGroup(usergroup);

			if (conferences.Length < 1)
			{
				return RedirectToAction<ConferenceController>(c => c.New(null));
			}

			object conferenceListDto = Mapper.Map(conferences, typeof (Conference[]), typeof (ConferenceForm[]));
			return View(conferenceListDto);
		}

		[RequireAuthenticationFilter()]
		public ActionResult Edit(Conference conference)
		{
			if (conference == null)
			{
				TempData.Add("message", "Conference has been deleted.");
				return RedirectToAction<ConferenceController>(c => c.List(conference.UserGroup));
			}

            if(_securityContext.HasPermissionsFor(conference))
            {
                return View(_mapper.Map(conference));
            }
		    return View(ViewPages.NotAuthorized);
		}

		[RequireAuthenticationFilter()]
		[ValidateInput(false)]
		[ValidateModel(typeof (ConferenceForm))]
		public ActionResult Save([Bind(Prefix = "")] ConferenceForm form)
		{
            if (_securityContext.HasPermissionsForUserGroup(form.UserGroupId))
            {
                return ProcessSave(form, conference => RedirectToAction<ConferenceController>(c => c.List(conference.UserGroup)));
            }
		    return View(ViewPages.NotAuthorized);
		}

		protected override IDictionary<string, string[]> GetFormValidationErrors(ConferenceForm form)
		{
			var result = new ValidationResult();
			if (ConferenceKeyAlreadyExists(form))
			{
				result.AddError<ConferenceForm>(x => x.Key, "This conference key already exists");
			}
			return result.GetAllErrors();
		}

		private bool ConferenceKeyAlreadyExists(ConferenceForm message)
		{
			Conference conference = _repository.GetByKey(message.Key);
			return conference != null && conference.Id != message.Id;
		}

		[RequireAuthenticationFilter()]
		public ActionResult New(UserGroup usergroup)
		{
			return View("Edit", _mapper.Map(new Conference {UserGroup = usergroup}));
		}
	}
}