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
	[AdminUserCreatedFilter]
	public class ConferenceController : SmartController
	{
		private readonly IConferenceMapper _mapper;
		private readonly IConferenceRepository _repository;
		private readonly ISecurityContext _securityContext;

		public ConferenceController(IConferenceRepository repository, IConferenceMapper mapper,
		                            ISecurityContext securityContext)
			
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
		}

		[RequiresConferenceFilter]
		public ActionResult Index(Conference conference)
		{
			ConferenceInput input = _mapper.Map(conference);
			return View(input);
		}

		public ActionResult List(UserGroup usergroup)
		{
			Conference[] conferences = _repository.GetAllForUserGroup(usergroup);

			if (conferences.Length < 1)
			{
				return RedirectToAction<ConferenceController>(c => c.New(null));
			}

			ConferenceInput[] conferenceListDto = _mapper.Map(conferences);
			return View(conferenceListDto);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[RequireAuthenticationFilter]
		public ActionResult Edit(Conference conference)
		{
			if (conference == null)
			{
				TempData.Add("message", "Conference has been deleted.");

				return RedirectToAction<ConferenceController>(c => c.List(conference.UserGroup));
			}

			if (_securityContext.HasPermissionsFor(conference))
			{
				return View(_mapper.Map(conference));
			}
			return View(ViewPages.NotAuthorized);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		[ValidateModel(typeof (ConferenceInput))]
		public ActionResult Edit(ConferenceInput input)
		{
			throw new Exception("Not Implemented");
			if (_securityContext.HasPermissionsForUserGroup(input.UserGroupId))
			{
				//return ProcessSave(input, conference => RedirectToAction<HomeController>(c => c.Index(conference.UserGroup)));
			}
			return View(ViewPages.NotAuthorized);
		}

		//protected override IDictionary<string, string[]> GetFormValidationErrors(ConferenceInput input)
		//{
		//    var result = new ValidationResult();
		//    if (ConferenceKeyAlreadyExists(input))
		//    {
		//        result.AddError<ConferenceInput>(x => x.Key, "This conference key already exists");
		//    }
		//    return result.GetAllErrors();
		//}

		private bool ConferenceKeyAlreadyExists(ConferenceInput message)
		{
			Conference conference = _repository.GetByKey(message.Key);
			return conference != null && conference.Id != message.Id;
		}

		[RequireAuthenticationFilter]
		public ActionResult New(UserGroup usergroup)
		{
			return View("Edit", _mapper.Map(new Conference {UserGroup = usergroup}));
		}

		public ActionResult Delete(Conference conference)
		{
			if (!_securityContext.HasPermissionsFor(conference))
			{
				return NotAuthorizedView;
			}

			_repository.Delete(conference);

			TempData.Add("message", conference.Name + " was deleted.");

			return RedirectToAction<HomeController>(c => c.Index(conference.UserGroup));
		}
	}
}