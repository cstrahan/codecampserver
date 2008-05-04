using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{    
    [AdminOnly]
    public class AdminController : Controller
    {
        private readonly IConferenceService _conferenceService;        

        public AdminController(IAuthorizationService authorizationService, IConferenceService conferenceService) : base(authorizationService)
        {
            _conferenceService = conferenceService;
        }
        
        public ActionResult Index()
        {            
            var conferences = _conferenceService.GetAllConferences();
            ViewData.Add(conferences);
            return RenderView();
        }
        
        public ActionResult Schedule()
        {
            return RenderView();
        }
    }
}
