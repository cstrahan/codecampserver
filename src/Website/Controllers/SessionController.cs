using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Helpers;
using CodeCampServer.Website.Views;
using MvcContrib.Filters;

namespace CodeCampServer.Website.Controllers
{
	public class SessionController : Controller
	{
		private readonly ISessionService _sessionService;
		private readonly IUserSession _userSession;
	    private readonly IPersonRepository _personRepository;
	    private readonly IConferenceRepository _conferenceRepository;

	    public SessionController(IConferenceRepository conferenceRepository, ISessionService sessionService, IPersonRepository personRepository, IAuthorizationService authorizationService, IUserSession userSession)
			: base(authorizationService)
		{
	        _conferenceRepository = conferenceRepository;
			_sessionService = sessionService;
	        _personRepository = personRepository;
	        _userSession = userSession;
		}
        
        //TODO:  change this action to just be 'new'
        [RequireLogin]        
		public ActionResult Create(string conferenceKey)
		{
		    var speaker = getLoggedInSpeaker(conferenceKey);
            if (speaker == null)
            {                
                return RedirectToAction( new {
                    controller = "login",
                    action = "index",
                    redirectUrl = ControllerContext.HttpContext.Request.Url.PathAndQuery
                });
            }
			
		    ViewData.Add(speaker);
			return RenderView();
		}

        //TODO:  change this action to just be 'create'
        [RequireLogin]
        [PostOnly]
	    public ActionResult CreateNew(string conferenceKey, string speakerEmail, string title, string @abstract)
		{		    
		    var person = _personRepository.FindByEmail(speakerEmail);		    

		    var session = _sessionService.CreateSession(null, person, title, @abstract, null);
			ViewData.Add(session);

            return RenderView("CreateConfirm");
		}
        
	    public ActionResult Proposed(string conferenceKey)
		{
			var conference = getConferenceByKey(conferenceKey);
			var sessions = _sessionService.GetProposedSessions(conference);
			ViewData.Add(conference);
			ViewData.Add(sessions);

			return RenderView();
		}

	    private Speaker getLoggedInSpeaker(string conferenceKey)
	    {
	        var p = _userSession.GetLoggedInPerson();
	        if(p == null) return null;

	        var conf = getConferenceByKey(conferenceKey);
	        return p.GetSpeakerProfileFor(conf);
	    }

        private Conference getConferenceByKey(string conferenceKey)
        {
            return _conferenceRepository.GetConferenceByKey(conferenceKey);
        }
	}
}