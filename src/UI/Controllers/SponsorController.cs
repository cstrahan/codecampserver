using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class SponsorController : ConventionController
	{
		private readonly IUserGroupRepository _repository;
		private readonly IUserGroupSponsorMapper _mapper;
		private readonly ISecurityContext _securityContext;

		public SponsorController(IUserGroupRepository repository, IUserGroupSponsorMapper mapper,
		                         ISecurityContext securityContext)

		{
			_repository = repository;
			_mapper = mapper;
			_securityContext = securityContext;
		}


		public ActionResult Index(UserGroup usergroup)
		{
			UserGroup group = _repository.GetById(usergroup.Id);

			Sponsor[] entities = group.GetSponsors();

			SponsorInput[] displayModel = _mapper.Map(entities);

			return View(displayModel);
		}


		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult Edit(UserGroup userGroup, SponsorInput sponsorInput)
		{
			return Command<SponsorInput, Sponsor>(sponsorInput,
			                                      r => RedirectToAction<SponsorController>(c => c.Index(null)),
			                                      input => View(sponsorInput));
		}

		public ActionResult Delete(UserGroup userGroup, Sponsor sponsor)
		{
			userGroup.Remove(sponsor);
			_repository.Save(userGroup);
			return RedirectToAction<SponsorController>(c => c.Index(null));
		}


		[HttpGet]
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