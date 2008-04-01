using CodeCampServer.Model;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{    
    [AuthorizationFilter(AllowRoles="Administrator,Organizer", Order=1)]
    public class AdminController : Controller
    {
        private IConferenceService _conferenceService;        

        public AdminController(IAuthorizationService authorizationService, IConferenceService conferenceService) : base(authorizationService)
        {
            _conferenceService = conferenceService;
        }
        
        public void Index()
        {
            RenderView("Index");            
        }
        
        public void Schedule()
        {                          
            RenderView("Schedule");
        }
    }
}
