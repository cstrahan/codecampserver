using System;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
    public class AdminController : ApplicationController
    {
        private IConferenceService _conferenceService;        

        public AdminController(IAuthorizationService authorizationService, IConferenceService conferenceService) : base(authorizationService)
        {
            _conferenceService = conferenceService;
        }

        [ControllerAction]
        public void Schedule()
        {                          
            RenderView("Schedule");
        }
    }
}
