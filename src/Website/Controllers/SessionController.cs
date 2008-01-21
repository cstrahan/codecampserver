using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Views;
using Iesi.Collections.Generic;

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
        public void Register(string conferenceKey)
        {
            IEnumerable<Speaker> speakers = 
                _conferenceService.GetSpeakers(_conferenceService.GetConference(conferenceKey), 0, int.MaxValue);

            RenderView("Register", new SmartBag(new SpeakerListingCollection(speakers)));
        }

        [ControllerAction]
        public void Create( string conferenceKey, string speakerKey, 
                            string title, string @abstract,
                            string blogName, string blogUrl,
                            string websiteName, string websiteUrl, 
                            string downloadName, string downloadUrl)
        {
            Speaker speaker = _conferenceService.GetSpeakerByDisplayName(speakerKey);
            
            ISet<OnlineResource> onlineResources = new HashedSet<OnlineResource>();
            if (!String.IsNullOrEmpty(blogUrl))
                onlineResources.Add(new OnlineResource(OnlineResourceType.Blog, blogName, blogUrl));
            if (!String.IsNullOrEmpty(websiteUrl))
                onlineResources.Add(new OnlineResource(OnlineResourceType.Website, websiteName, websiteUrl));
            if (!String.IsNullOrEmpty(downloadUrl))
                onlineResources.Add(new OnlineResource(OnlineResourceType.Download, downloadName, downloadUrl));

            Session session = _conferenceService.CreateSession(speaker, title, @abstract, onlineResources);

            RenderView("RegisterConfirm", new SmartBag(session));
        }

        private static ISet<OnlineResource> ParseOnlineResources(string onlineResources)
        {
            HashedSet<OnlineResource> resourceList = new HashedSet<OnlineResource>();

            OnlineResource newResource = new OnlineResource();
            foreach (string resource in onlineResources.Split('\n'))
            {
                string[] resourceParts = resource.Split('|');
                if(resourceParts.Length == 3)
                {
                    newResource.Type = (OnlineResourceType)Enum.Parse(typeof(OnlineResourceType), resourceParts[0]);
                    newResource.Name = resourceParts[1];
                    newResource.Href = resourceParts[2];

                    resourceList.Add(newResource);
                }
            }

            return resourceList;
        }

    }
}
