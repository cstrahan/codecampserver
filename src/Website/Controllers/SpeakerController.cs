using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
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
        private readonly ISpeakerService _speakerService;
        private readonly IClock _clock;
		private IUserSession _userSession;

		public SpeakerController(IConferenceService conferenceService,
                                 ISpeakerService speakerService,
                                 IAuthorizationService authorizationService,
		                         IClock clock, IUserSession userSession)
			: base(authorizationService)
		{
			_conferenceService = conferenceService;
            _speakerService = speakerService;
			_clock = clock;
			_userSession = userSession;
		}

		public void Index(string conferenceKey)
		{
			RedirectToAction(new RouteValueDictionary(new
			{
				Controller = "conference",
				Action = "details",
				conferenceKey = conferenceKey
			}));
		}

		public void List(string conferenceKey, int? page, int? perPage)
		{
			int effectivePage = page.GetValueOrDefault(0);
			int effectivePerPage = perPage.GetValueOrDefault(20);

			Conference conference = _conferenceService.GetConference(conferenceKey);
			ScheduledConference scheduledConference = new ScheduledConference(conference, _clock);
            IEnumerable<Speaker> speakers = _speakerService.GetSpeakers(conference, effectivePage, effectivePerPage);
			SpeakerListingCollection speakerListings = new SpeakerListingCollection(speakers);

			SmartBag.Add(scheduledConference);
			SmartBag.Add(speakerListings);
			SmartBag.Add("page", effectivePage);
			SmartBag.Add("perPage", effectivePerPage);

			RenderView("List");
		}

		public void View(string conferenceKey, string speakerId)
		{
            Speaker speaker = _speakerService.GetSpeakerByDisplayName(speakerId);
			SmartBag.Add(speaker);
			RenderView("view");
		}

		public void Edit()
		{
			Speaker speaker = _userSession.GetLoggedInSpeaker();
			if (speaker != null)
			{
				SmartBag.Add(speaker);
				RenderView("edit");
			}
			else
			{
				RedirectToAction(new RouteValueDictionary(new
				{
					Controller = "login",
					Action = "Index"
				}));
			}
		}

		public void Save(string conferenceKey, string displayName, string firstName, string lastName, string website,
		                 string comment, string profile, string avatarUrl)
		{
			Attendee user = _userSession.GetCurrentUser();
			try
			{
                _speakerService.SaveSpeaker(user.Contact.Email, firstName, lastName, website, comment, displayName, profile, avatarUrl);

				TempData["message"] = "Profile saved";
				RedirectToAction(new RouteValueDictionary(new
				{
					Action = "view",
					conferenceKey = conferenceKey,
					speakerId = displayName
				}));
			}
			catch (DataValidationException ex)
			{
				TempData["error"] = ex.Message;
				RedirectToAction("edit");
			}
		}
	}
}