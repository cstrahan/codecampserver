using System.Web.Mvc;
using AutoMapper;
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
		private readonly IMappingEngine _mappingEngine;
		private readonly ISecurityContext _securityContext;
		private IMeetingMapper _meetingMapper;

		public MeetingController(IMeetingRepository repository, IMeetingMapper mapper,
		                         IMeetingRepository meetingRepository, IMappingEngine mappingEngine,
		                         ISecurityContext securityContext, IMeetingMapper meetingMapper) : base(repository, mapper)
		{
			_meetingRepository = meetingRepository;
			_mappingEngine = mappingEngine;
			_securityContext = securityContext;
			_meetingMapper = meetingMapper;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Edit(Meeting meeting, UserGroup usergroup)
		{
			var input = new MeetingInput();
			if (meeting == null)
			{
				_mappingEngine.Map(new Meeting {UserGroup = usergroup}, input);
				return View(input);
			}

			_mappingEngine.Map(meeting, input);
			return View(input);
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