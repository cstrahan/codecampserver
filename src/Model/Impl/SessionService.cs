using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Impl
{
	public class SessionService : ISessionService
	{
		private readonly ISessionRepository _sessionRepository;

		public SessionService(ISessionRepository sessionRepository)
		{
			_sessionRepository = sessionRepository;
		}

		public Session CreateSession(Conference conference, Person speaker, string title, string @abstract, Track track)
		{
			Session session = new Session(conference, speaker, title, @abstract, track);
			_sessionRepository.Save(session);
			return session;
		}

		public IEnumerable<Session> GetProposedSessions(Conference conference)
		{
			return _sessionRepository.GetProposedSessions(conference);
		}
	}
}