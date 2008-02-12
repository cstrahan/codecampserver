using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
    [PluginFamily(Keys.DEFAULT)]
    public interface ISessionService
    {
        Session CreateSession(Speaker speaker, string title, string @abstract, OnlineResource[] onlineResources);

        //TODO:  This is a duplicate of ISessionRepository -Palermo
        IEnumerable<Session> GetProposedSessions(Conference conference);
    }
}
