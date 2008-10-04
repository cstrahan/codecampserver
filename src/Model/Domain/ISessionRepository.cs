using System.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    public interface ISessionRepository
	{
	    void Save(Session session);
        Session[] GetProposedSessions(Conference conference);
        Session[] GetUnallocatedApprovedSessions(Conference conference);
	}
}
