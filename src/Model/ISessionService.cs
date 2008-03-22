using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model
{    
    public interface ISessionService
    {
        Session CreateSession(Conference conference, Person speaker, string title, string @abstract, Track track);

        //TODO:  This is a duplicate of ISessionRepository -Palermo
        IEnumerable<Session> GetProposedSessions(Conference conference);
    }
}
