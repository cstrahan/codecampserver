using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IConferenceService
	{
		Conference GetConference(string conferenceKey);
		Attendee[] GetAttendees(Conference conference, int page, int perPage);
		Attendee RegisterAttendee(string firstName, string lastName, string website, string comment, Conference conference, string emailAddress, string cleartextPassword);
		Speaker GetSpeakerByDisplayName(string displayName);
        IEnumerable<Speaker> GetSpeakers(Conference conference, int page, int perPage);
        Speaker GetLoggedInSpeaker();
        Speaker SaveSpeaker(string emailAddress, string firstName, string lastName, string website, string comment, string displayName, string profile, string avatarUrl);

        string GetLoggedInUsername();
    }
}
