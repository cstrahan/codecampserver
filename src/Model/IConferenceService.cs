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
	}
}