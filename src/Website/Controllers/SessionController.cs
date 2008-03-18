using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class SessionController : ApplicationController
	{
		private readonly IConferenceService _conferenceService;
		private readonly ISessionService _sessionService;
		private readonly IUserSession _userSession;
	    private IPersonRepository _personRepository;

	    public SessionController(IConferenceService conferenceService,
		                         ISessionService sessionService,
		                         IPersonRepository personRepository,
		                         IAuthorizationService authorizationService,
		                         IUserSession userSession)
			: base(authorizationService)
		{
			_conferenceService = conferenceService;
			_sessionService = sessionService;
	        _personRepository = personRepository;
	        _userSession = userSession;
		}

		public void Create(string conferenceKey)
		{
		    Speaker currentUser = getLoggedInSpeaker(conferenceKey);

			if (currentUser == null)
				RedirectToAction("index", "login");
			else
			{
				SmartBag.Add(currentUser);
				RenderView("Create");
			}
		}

	    private Speaker getLoggedInSpeaker(string conferenceKey)
	    {
	        Person p = _userSession.GetLoggedInPerson();
            if(p == null) return null;

	        Conference conf = _conferenceService.GetConference(conferenceKey);
	        return p.GetSpeakerProfileFor(conf);
	    }

	    public void CreateNew(string conferenceKey, string speakerEmail,
		                      string title, string @abstract)
		{
		    
		    Person person = _personRepository.FindByEmail(speakerEmail);		    

		    Session session = _sessionService.CreateSession(null, person, title, @abstract, null);
			SmartBag.Add(session);

			RenderView("CreateConfirm");
		}

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