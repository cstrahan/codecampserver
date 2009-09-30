using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class MeetingController : SaveController<Meeting, MeetingInput>
	{
		private readonly IMeetingRepository _meetingRepository;
		private readonly IMeetingMapper _meetingMapper;
		private readonly ISecurityContext _securityContext;

		public MeetingController(IMeetingRepository meetingRepository, IMeetingMapper meetingMapper,
		                         ISecurityContext securityContext)
			: base(meetingRepository, meetingMapper)
		{
			_meetingRepository = meetingRepository;
			_meetingMapper = meetingMapper;
			_securityContext = securityContext;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Edit(Meeting meeting,UserGroup usergroup)
		{
			if(meeting==null)
			{
				return View(_meetingMapper.Map(new Meeting { UserGroup = usergroup }));
			}

			MeetingInput model = _meetingMapper.Map(meeting);
			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		[ValidateModel(typeof (MeetingInput))]
		public ActionResult Edit(MeetingInput input)
		{
			if (_securityContext.HasPermissionsForUserGroup(input.UserGroupId))
			{
				return ProcessSave(input, meeting => RedirectToAction<HomeController>(c => c.Index(meeting.UserGroup)));
			}

			return View(ViewPages.NotAuthorized);
		}

		[RequireAuthenticationFilter]
		public ActionResult Delete(Meeting meeting)
		{
			if (!_securityContext.HasPermissionsFor(meeting))
			{
				return NotAuthorizedView;
			}

			_meetingRepository.Delete(meeting);

			TempData.Add("message", meeting.Name + " was deleted.");

			return RedirectToAction<HomeController>(c => c.Index(meeting.UserGroup));
		}

		public ActionResult New(UserGroup usergroup)
		{
			return RedirectToAction<MeetingController>(c => c.Edit(null, null));
		}
	}
}