using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
    [Pluggable(Keys.DEFAULT)]
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public Session CreateSession(Speaker speaker, string title,
                                 string @abstract, OnlineResource[] onlineResources)
        {
            Session session = new Session(speaker, title, @abstract, onlineResources);
            _sessionRepository.Save(session);
            return session;
        }

        public IEnumerable<Session> GetProposedSessions(Conference conference)
        {
            return _sessionRepository.GetProposedSessions(conference);
        }
    }
}