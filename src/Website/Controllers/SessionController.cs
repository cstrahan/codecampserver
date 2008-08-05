using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using MvcContrib;
using MvcContrib.Filters;

namespace CodeCampServer.Website.Controllers
{
	public class SessionController : Controller
	{
		private readonly ISessionRepository _sessionRepository;
		private readonly IUserSession _userSession;
		private readonly IPersonRepository _personRepository;
		private readonly IConferenceRepository _conferenceRepository;

		public SessionController(IConferenceRepository conferenceRepository, ISessionRepository sessionRepository,
		                         IPersonRepository personRepository, IUserSession userSession)
			: base(userSession)
		{
			_conferenceRepository = conferenceRepository;
			_sessionRepository = sessionRepository;
			_personRepository = personRepository;
			_userSession = userSession;
		}

		//TODO:  change this action to just be 'new'
		[Authorize]
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
		[Authorize]
		[PostOnly]
		public ActionResult CreateNew(string conferenceKey, string speakerEmail, string title, string @abstract)
		{
			Person person = _personRepository.FindByEmail(speakerEmail);

			Session session = new Session(null, person, title, @abstract, null);
			_sessionRepository.Save(session);
			ViewData.Add(session);

			return View("CreateConfirm");
		}

		public ActionResult Proposed(string conferenceKey)
		{
			Conference conference = getConferenceByKey(conferenceKey);
			IEnumerable<Session> sessions = _sessionRepository.GetProposedSessions(conference);
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