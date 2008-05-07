using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Views;

namespace CodeCampServer.Website.Controllers
{    
    [AdminOnly]
    public class AdminController : Controller
    {
        private readonly IConferenceRepository _conferenceRepository;

        public AdminController(IAuthorizationService authorizationService, IConferenceRepository conferenceRepository) : base(authorizationService)
        {
            _conferenceRepository = conferenceRepository;
        }
        
        public ActionResult Index()
        {
            var conferences = _conferenceRepository.GetAllConferences();
            ViewData.Add(conferences);
            return RenderView();
        }
        
        public ActionResult Schedule()
        {
            return RenderView();
        }
    }
}
