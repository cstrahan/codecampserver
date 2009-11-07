using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class MeetingController : ConventionController
	{
		private readonly IMeetingMapper _mapper;
		private readonly ISecurityContext _securityContext;

		public MeetingController(IMeetingMapper meetingMapper, ISecurityContext securityContext)
		{
			_mapper = meetingMapper;
			_securityContext = securityContext;
		}

		[HttpGet]
		public ActionResult Edit(Meeting meeting, UserGroup usergroup)
		{
			MeetingInput input;
			if (meeting == null)
			{
				input = _mapper.Map(new Meeting {UserGroup = usergroup});
				return View(input);
			}

			input = _mapper.Map(meeting);
			return View(input);
		}

		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
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