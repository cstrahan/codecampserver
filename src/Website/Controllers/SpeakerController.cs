using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Exceptions;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class SpeakerController : ApplicationController
	{
		private readonly IConferenceService _conferenceService;
		private readonly IClock _clock;
		private IUserSession _userSession;

		public SpeakerController(IConferenceService conferenceService, IAuthorizationService authorizationService,
		                         IClock clock, IUserSession userSession)
			: base(authorizationService)
		{
			_conferenceService = conferenceService;
			_clock = clock;
			_userSession = userSession;
		}

		[ControllerAction]
		public void Index(string conferenceKey)
		{
			RedirectToAction(new
			{
				Controller = "conference",
				Action = "details",
				conferenceKey = conferenceKey
			});
		}

		[ControllerAction]
		public void List(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			Conference conference = _conferenceService.GetConference(conferenceKey);
			ScheduledConference scheduledConference = new ScheduledConference(conference, _clock);
			IEnumerable<Speaker> speakers = _conferenceService.GetSpeakers(conference, effectivePage, effectivePerPage);
			SpeakerListingCollection speakerListings = new SpeakerListingCollection(speakers);

			SmartBag.Add(scheduledConference);
			SmartBag.Add(speakerListings);
			SmartBag.Add("page", effectivePage);
			SmartBag.Add("perPage", effectivePerPage);

			RenderView("List");
		}

		[ControllerAction]
		public void View(string conferenceKey, string speakerId)
		{
			Speaker speaker = _conferenceService.GetSpeakerByDisplayName(speakerId);
			SmartBag.Add(speaker);
			RenderView("view");
		}

		[ControllerAction]
		public void Edit()
		{
			Speaker speaker = _conferenceService.GetLoggedInSpeaker();
			if (speaker != null)
			{
				SmartBag.Add(speaker);
				RenderView("edit");
			}
			else
			{
				RedirectToAction(new
				{
					Controller = "login",
					Action = "Index"
				});
			}
		}

		[ControllerAction]
		public void Save(string conferenceKey, string displayName, string firstName, string lastName, string website,
		                 string comment, string profile, string avatarUrl)
		{
			Attendee user = _userSession.GetCurrentUser();
			try
			{
				_conferenceService.SaveSpeaker(user.Contact.Email, firstName, lastName, website, comment, displayName, profile, avatarUrl);

				TempData["message"] = "Profile saved";
				RedirectToAction(new
				{
					Action = "view",
					conferenceKey = conferenceKey,
					speakerId = displayName
				});
			}
			catch (DataValidationException ex)
			{
				TempData["error"] = ex.Message;
				RedirectToAction("edit");
			}
		}
	}
}