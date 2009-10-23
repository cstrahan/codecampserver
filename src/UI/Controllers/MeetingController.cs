using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Messages;
using CodeCampServer.UI.Models.Input;
using CommandProcessor;

namespace CodeCampServer.UI.Controllers
{
	public class MeetingController : SmartController
	{
		private readonly IMeetingMapper _mapper;
		private readonly ISecurityContext _securityContext;
		private readonly IRulesEngine _rulesEngine;

		public MeetingController(IMeetingMapper meetingMapper, ISecurityContext securityContext, IRulesEngine rulesEngine)
		{
			_mapper = meetingMapper;
			_securityContext = securityContext;
			_rulesEngine = rulesEngine;
		}

		[AcceptVerbs(HttpVerbs.Get)]
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

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		[ValidateModel(typeof (MeetingInput))]
		public ActionResult Edit(MeetingInput input)
		{
			if (!_securityContext.HasPermissionsForUserGroup(input.UserGroupId))
			{
				return View(ViewPages.NotAuthorized);
			}

			if (ModelState.IsValid)
			{
				var result = _rulesEngine.Process(input); 
				if (result.Successful)
				{
					var meeting = result.ReturnItems.Get<Meeting>();
					return RedirectToAction<HomeController>(c => c.Index(meeting.UserGroup));
				}				
			}
			return View(input);
		}

		[RequireAuthenticationFilter]
		public ActionResult Delete(DeleteMeetingMessage message,UserGroup userGroup)
		{
			if (!_securityContext.HasPermissionsFor(userGroup))
			{
				return NotAuthorizedView;
			}

			var result = _rulesEngine.Process(message);
			
			if(result.Successful)
			{
				TempData.Add("message", result.ReturnItems.Get<Meeting>().Name + " was deleted.");
			}
			else
			{
				TempData.Add("message", result.Messages[0]);
			}
			return RedirectToAction<HomeController>(c => c.Index(userGroup));			
		}

		public ActionResult New(UserGroup usergroup)
		{
			return RedirectToAction<MeetingController>(c => c.Edit(null, null));
		}
	}
}