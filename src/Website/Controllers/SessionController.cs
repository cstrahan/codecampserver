using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{
    public class SessionController : Controller
    {
        private IConferenceService _conferenceService;

        public SessionController(IConferenceService conferenceService)
        {
            _conferenceService = conferenceService;
        }

        [ControllerAction]
        public void Create(string conferenceKey)
        {
            Speaker currentUser = _conferenceService.GetLoggedInSpeaker();

            if (currentUser == null)
                RedirectToAction(new { Controller = "login", Action="index" });
            else
                RenderView("Create", new SmartBag().Add(currentUser));
        }

        [ControllerAction]
        public void CreateNew( string conferenceKey, string speakerEmail, 
                            string title, string @abstract,
                            string blogName, string blogUrl,
                            string websiteName, string websiteUrl, 
                            string downloadName, string downloadUrl)
        {
            Speaker speaker = _conferenceService.GetSpeakerByEmail(speakerEmail);

            List<OnlineResource> onlineResources = new List<OnlineResource>();
            onlineResources.Add(new OnlineResource(OnlineResourceType.Blog, blogName, blogUrl));
            onlineResources.Add(new OnlineResource(OnlineResourceType.Website, websiteName, websiteUrl));
            onlineResources.Add(new OnlineResource(OnlineResourceType.Download, downloadName, downloadUrl));

            Session session = _conferenceService.CreateSession(speaker, title, @abstract, onlineResources.ToArray());

            RenderView("CreateConfirm", new SmartBag().Add(session));
        }

        [ControllerAction]
        public void Proposed(string conferenceKey)
        {
            Conference conference = _conferenceService.GetConference(conferenceKey);
            IEnumerable<Session> sessions = _conferenceService.GetProposedSessions(conference);

            SmartBag values = new SmartBag();
            values.Add(conference);
            values.Add(sessions);

            RenderView("Proposed", values);
        }
    }
}
