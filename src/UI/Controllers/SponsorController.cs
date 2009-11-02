using System;
using System.Linq;
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
	public class SponsorController : SmartController
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupSponsorMapper _mapper;
		private readonly ISecurityContext _securityContext;
		private readonly IRulesEngine _rulesEngine;

		public SponsorController(IUserGroupRepository repository, IUserGroupSponsorMapper mapper, ISecurityContext securityContext, IRulesEngine rulesEngine)
			
		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
			_rulesEngine = rulesEngine;
		}


		public ActionResult Index(UserGroup usergroup)
		{
			var group = _repository.GetById(usergroup.Id);

			Sponsor[] entities = group.GetSponsors();

			SponsorInput[] displayModel = _mapper.Map(entities);

			return View(displayModel);
		}


		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		//[ValidateModel(typeof (SponsorInput))]
		public ActionResult Edit(UserGroup userGroup, SponsorInput sponsorInput)
		{
			if (ModelState.IsValid)
			{
				ExecutionResult result = _rulesEngine.Process(sponsorInput);
				if (result.Successful)
				{
					return RedirectToAction<SponsorController>(c => c.Index(null));
				}

				foreach (var errorMessage in result.Messages)
				{
					ModelState.AddModelError(errorMessage.IncorrectAttribute, errorMessage.MessageText);
				}
			}
			return View(sponsorInput);
		}

		public ActionResult Delete(UserGroup userGroup, Sponsor sponsor)
		{
			userGroup.Remove(sponsor);
			_repository.Save(userGroup);
			return RedirectToAction<SponsorController>(c => c.Index(null));
		}


		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult Edit(UserGroup userGroup, Sponsor sponsor)
		{
			if (!_securityContext.IsAdmin())
			{
				return NotAuthorizedView;
			}

			if (sponsor == null)
			{
				sponsor = new Sponsor();
			}

			SponsorInput input = _mapper.Map(sponsor);
			input.UserGroupId = userGroup.Id;

			return View(input);
		}

		public ActionResult List(UserGroup userGroup)
		{
			Sponsor[] entities = userGroup.GetSponsors();

			SponsorInput[] entityListDto = _mapper.Map(entities);

			return View("HomePageWidget", entityListDto);
		}
	}
}