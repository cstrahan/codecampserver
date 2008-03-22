using System.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    public interface ISessionRepository
	{
	    void Save(Session session);
        IEnumerable<Session> GetProposedSessions(Conference conference);
	}
}
