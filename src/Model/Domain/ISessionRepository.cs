using System.Collections.Generic;
using StructureMap;

namespace CodeCampServer.Model.Domain
{
    [PluginFamily(Keys.DEFAULT)]
    public interface ISessionRepository
	{
	    void Save(Session session);
        IEnumerable<Session> GetProposedSessions(Conference conference);
	}
}
