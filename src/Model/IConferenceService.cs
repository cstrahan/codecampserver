using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
	[PluginFamily("Default")]
	public interface IConferenceService
	{
		Conference GetConference(string conferenceKey);
		void RegisterAttendee(Attendee attendee);
		IEnumerable<Attendee> GetAttendees(Conference conference, int page, int perPage);
	}
}