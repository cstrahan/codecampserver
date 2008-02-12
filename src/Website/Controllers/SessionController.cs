using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
    public class SessionController : ApplicationController
    {
        private readonly IConferenceService _conferenceService;
        private readonly ISessionService _sessionService;
        private readonly ISpeakerService _speakerService;
        private readonly IUserSession _userSession;

        public SessionController(IConferenceService conferenceService, 
                                 ISessionService sessionService,
                                 ISpeakerService speakerService,
                                 IAuthorizationService authorizationService,
                                 IUserSession userSession)
            : base(authorizationService)
        {
            _conferenceService = conferenceService;
            _sessionService = sessionService;
            _speakerService = speakerService;
            _userSession = userSession;
        }

        [ControllerAction]
        public void Create(string conferenceKey)
        {
            Speaker currentUser = _userSession.GetLoggedInSpeaker();

            if (currentUser == null)
                RedirectToAction(new { Controller = "login", Action="index" });
            else
            {
                SmartBag.Add(currentUser);
                RenderView("Create");
            }
        }

        [ControllerAction]
        public void CreateNew( string conferenceKey, string speakerEmail, 
                            string title, string @abstract,
                            string blogName, string blogUrl,
                            string websiteName, string websiteUrl, 
                            string downloadName, string downloadUrl)
        {
            Speaker speaker = _speakerService.GetSpeakerByEmail(speakerEmail);

            List<OnlineResource> onlineResources = new List<OnlineResource>();
            onlineResources.Add(new OnlineResource(OnlineResourceType.Blog, blogName, blogUrl));
            onlineResources.Add(new OnlineResource(OnlineResourceType.Website, websiteName, websiteUrl));
            onlineResources.Add(new OnlineResource(OnlineResourceType.Download, downloadName, downloadUrl));

            Session session = _sessionService.CreateSession(speaker, title, @abstract, onlineResources.ToArray());
            SmartBag.Add(session);

            RenderView("CreateConfirm");
        }

        [ControllerAction]
        public void Proposed(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            IEnumerable<Session> sessions = _sessionService.GetProposedSessions(conference);
            SmartBag.Add(conference);
            SmartBag.Add(sessions);

            RenderView("Proposed");
        }
    }
}
