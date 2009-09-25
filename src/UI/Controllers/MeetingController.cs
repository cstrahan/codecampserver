using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Controllers
{
	public class MeetingController : SaveController<Meeting, MeetingForm>
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
		public ActionResult Edit(Meeting meeting)
		{
			return View(_meetingMapper.Map(meeting));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[RequireAuthenticationFilter]
		[ValidateInput(false)]
		[ValidateModel(typeof (MeetingForm))]
		public ActionResult Edit(MeetingForm form)
		{
			if (_securityContext.HasPermissionsForUserGroup(form.UserGroupId))
			{
				return ProcessSave(form, meeting => RedirectToAction<HomeController>(c => c.Index(meeting.UserGroup)));
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
			return View("Edit", _meetingMapper.Map(new Meeting {UserGroup = usergroup}));
		}
	}
}