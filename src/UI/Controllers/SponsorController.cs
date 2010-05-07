using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class SponsorController : ConventionController
	{
		private readonly IUserGroupRepository _repository;
		private readonly ISecurityContext _securityContext;

		public SponsorController(IUserGroupRepository repository, ISecurityContext securityContext)
		{
			_repository = repository;
			_securityContext = securityContext;
		}

		public ActionResult Index(UserGroup usergroup)
		{
			UserGroup group = _repository.GetById(usergroup.Id);
			AutoMappedViewResult view = AutoMappedView<UpdateSponsorInput[]>(group.GetSponsors());
	
			return view;
		}

		[HttpPost]
		[Authorize]
		public ActionResult Edit(UpdateSponsorInput sponsorInput, UserGroup userGroup)
		{
			sponsorInput.UserGroup = userGroup;
			return Command<UpdateSponsorInput, Sponsor>(sponsorInput,
			                                      r => new RedirectToReturnUrlResult(),
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

			if (sponsor != null)
			{
				sponsor.UserGroup=userGroup;
			}

			return AutoMappedView<UpdateSponsorInput>(sponsor??new Sponsor(){UserGroup=userGroup});
		}

		public ActionResult List(UserGroup userGroup)
		{
			var result = AutoMappedView<UpdateSponsorInput[]>(userGroup.GetSponsors());
			result.ViewName = "HomePageWidget";

			return result;
		}
	}
}