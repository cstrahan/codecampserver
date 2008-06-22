using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
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

		public SessionController(IConferenceRepository conferenceRepository, ISessionService sessionService,
		                         IPersonRepository personRepository, IAuthorizationService authorizationService,
		                         IUserSession userSession)
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
			Speaker speaker = getLoggedInSpeaker(conferenceKey);
			if (speaker == null)
			{
				return RedirectToAction("index", "login", new
				                                          	{
				                                          		redirectUrl = ControllerContext.HttpContext.Request.Url.PathAndQuery
				                                          	});
			}

			ViewData.Add(speaker);
			return View();
		}

		//TODO:  change this action to just be 'create'
		[RequireLogin]
		[PostOnly]
		public ActionResult CreateNew(string conferenceKey, string speakerEmail, string title, string @abstract)
		{
			Person person = _personRepository.FindByEmail(speakerEmail);

			Session session = _sessionService.CreateSession(null, person, title, @abstract, null);
			ViewData.Add(session);

			return View("CreateConfirm");
		}

		public ActionResult Proposed(string conferenceKey)
		{
			Conference conference = getConferenceByKey(conferenceKey);
			IEnumerable<Session> sessions = _sessionService.GetProposedSessions(conference);
			ViewData.Add(conference);
			ViewData.Add(sessions);

			return View();
		}

		private Speaker getLoggedInSpeaker(string conferenceKey)
		{
			Person p = _userSession.GetLoggedInPerson();
			if (p == null) return null;

			Conference conf = getConferenceByKey(conferenceKey);
			return p.GetSpeakerProfileFor(conf);
		}

		private Conference getConferenceByKey(string conferenceKey)
		{
			return _conferenceRepository.GetConferenceByKey(conferenceKey);
		}
	}
}