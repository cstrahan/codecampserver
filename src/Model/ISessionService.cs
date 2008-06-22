using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model
{
	public interface ISessionService
	{
		//TODO:  the caller of this method should use ISessionRepository directly
		Session CreateSession(Conference conference, Person speaker, string title, string @abstract, Track track);

		//TODO:  This is a duplicate of ISessionRepository -Palermo
		IEnumerable<Session> GetProposedSessions(Conference conference);
	}
}