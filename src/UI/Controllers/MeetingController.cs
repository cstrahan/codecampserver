using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class MeetingController : ConventionController
	{
		private readonly ISecurityContext _securityContext;

		public MeetingController(ISecurityContext securityContext)
		{
			_securityContext = securityContext;
		}

		[HttpGet]
		public ActionResult Edit(Meeting meeting, UserGroup usergroup)
		{
			return AutoMappedView<MeetingInput>(meeting ?? new Meeting {UserGroup = usergroup});
		}

		[HttpPost]
		[Authorize]
		[AllowHtml]
		public ActionResult Edit(MeetingInput input)
		{
			if (!_securityContext.HasPermissionsForUserGroup(input.UserGroupId))
			{
				return View(ViewPages.NotAuthorized);
			}
			return Command<MeetingInput, Meeting>(input,
			                                      r => RedirectToAction<HomeController>(c => c.Index(r.UserGroup)),
			                                      r => View(input));
		}

		[Authorize]
		public ActionResult Delete(DeleteMeetingMessage message, UserGroup userGroup)
		{
			if (!_securityContext.HasPermissionsFor(userGroup))
			{
				return NotAuthorizedView;
			}
			return Command(message, r => RedirectToAction<HomeController>(c => c.Index(userGroup)));
		}

		public ActionResult New(UserGroup usergroup)
		{
			return RedirectToAction<MeetingController>(c => c.Edit(null, null));
		}
	}
}