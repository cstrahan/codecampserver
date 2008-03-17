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

	    Person[] GetAttendees(Conference conference, int page, int perPage);
		Person RegisterAttendee(string firstName, string lastName, string emailAddress, string website, string comment, Conference conference, string cleartextPassword);
        Conference GetCurrentConference();
    }
}