using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IConferenceService
	{
		Conference GetConference(string conferenceKey);
		IEnumerable<Conference> GetAllConferences();

		Attendee[] GetAttendees(Conference conference, int page, int perPage);

		//TODO:  should be moved to IAttendeeService - Palermo
		Attendee RegisterAttendee(string firstName, string lastName, string website, string comment, Conference conference,
		                          string emailAddress, string cleartextPassword);

		//TODO:  Should go to IUserSession - Palermo
		Speaker GetLoggedInSpeaker();

		//TODO:  Should go to ISessionService -Palermo
		Session CreateSession(Speaker speaker, string title, string @abstract, OnlineResource[] onlineResources);
		//TODO:  This is a duplicate of ISessionRepository -Palermo
		IEnumerable<Session> GetProposedSessions(Conference conference);

		//TODO:  The below 3 are duplicates - Palermo
		Speaker GetSpeakerByDisplayName(string displayName);
		Speaker GetSpeakerByEmail(string email);
		IEnumerable<Speaker> GetSpeakers(Conference conference, int page, int perPage);

		Speaker SaveSpeaker(string emailAddress, string firstName, string lastName, string website, string comment,
		                    string displayName, string profile, string avatarUrl);
	}
}