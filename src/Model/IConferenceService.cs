using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IConferenceService
	{
		Conference GetConference(string conferenceKey);
		IEnumerable<Attendee> GetAttendees(Conference conference, int page, int perPage);
		Attendee RegisterAttendee(string firstName, string lastName, string website, string comment, Conference conference, string emailAddress, string cleartextPassword);
	}
}