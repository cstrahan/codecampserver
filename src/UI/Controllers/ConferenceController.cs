using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	[AdminUserCreatedFilter]
	public class ConferenceController : SaveController<Conference, ConferenceForm>
	{
		private readonly IConferenceMapper _mapper;
		private readonly IConferenceRepository _repository;
		private readonly ISecurityContext _securityContext;

		public ConferenceController(IConferenceRepository repository, IConferenceMapper mapper,
		                            ISecurityContext securityContext)
			: base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
		}

		[RequiresConferenceFilter]
		public ActionResult Index(Conference conference)
		{
			ConferenceForm form = _mapper.Map(conference);
			return View(form);
		}

		public ActionResult List(UserGroup usergroup)
		{
			Conference[] conferences = _repository.GetAllForUserGroup(usergroup);

			if (conferences.Length < 1)
			{
				return RedirectToAction<ConferenceController>(c => c.New(null));
			}

			object conferenceListDto = Mapper.Map(conferences, typeof (Conference[]), typeof (ConferenceForm[]));
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
		[ValidateModel(typeof (ConferenceForm))]
		public ActionResult Edit(ConferenceForm form)
		{
			if (_securityContext.HasPermissionsForUserGroup(form.UserGroupId))
			{
				return ProcessSave(form, conference => RedirectToAction<HomeController>(c => c.Index(conference.UserGroup)));
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