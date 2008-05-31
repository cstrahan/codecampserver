using System.Web.Mvc;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

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
            return RenderView();
        }
        
        public ActionResult Schedule()
        {
            return RenderView();
        }
    }
}
