using System;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using MvcContrib;
using MvcContrib.Attributes;
using MvcContrib.Filters;

namespace CodeCampServer.Website.Controllers
{
	public class SpeakerController : Controller
	{
		private readonly IClock _clock;
		private readonly IUserSession _userSession;
		private readonly IConferenceRepository _conferenceRepository;

		public SpeakerController(IConferenceRepository conferenceRepository,
		                         IUserSession userSession, IClock clock)
			: base(userSession)
		{
			_conferenceRepository = conferenceRepository;
			_clock = clock;
			_userSession = userSession;
		}

		[DefaultAction]
		public ActionResult List(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
			var scheduledConference = new Schedule(conference, _clock, null, null);
			Speaker[] speakers = conference.GetSpeakers();

			ViewData.Add(scheduledConference);
			ViewData.Add(speakers);
			ViewData.Add("page", effectivePage);
			ViewData.Add("perPage", effectivePerPage);

			return View();
		}

		public ActionResult Details(string conferenceKey, string speakerId)
		{
			Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
			Speaker speaker = conference.GetSpeakerByKey(speakerId);
			ViewData.Add(speaker);
			return View("view");
		}

		[Authorize]
		public ActionResult Edit(string conferenceKey)
		{
			Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
			Person person = _userSession.GetLoggedInPerson();

			Speaker speaker = null;
			if (person != null)
				speaker = person.GetSpeakerProfileFor(conference);

			if (speaker != null)
			{
				ViewData.Add(speaker);
				return View();
			}

			return RedirectToAction("index", "login");
		}

        [Authorize]
		[PostOnly]
		public ActionResult Save(string conferenceKey, string speakerKey, string bio, string avatarUrl)
		{
			Person person = _userSession.GetLoggedInPerson();
			try
			{
				Conference conference = _conferenceRepository.GetConferenceByKey(conferenceKey);
				conference.AddSpeaker(person, speakerKey, bio, avatarUrl);

				TempData[TempDataKeys.Message] = "Profile saved";
				return RedirectToAction("view", new {conferenceKey, speakerId = speakerKey});
			}
			catch (Exception ex)
			{
				TempData[TempDataKeys.Error] = "Error saving your speaker record.  The error has been logged.";
				Log.Error(this, "Error saving speaker.", ex);
				return RedirectToAction("edit");
			}
		}
	}
}