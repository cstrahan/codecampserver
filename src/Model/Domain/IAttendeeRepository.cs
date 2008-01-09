using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model.Domain
{
	[PluginFamily("Default")]
	public interface IAttendeeRepository
	{
		IEnumerable<Attendee> GetAttendeesForConference(Conference anConference);
		void Save(Attendee attendee);
		IEnumerable<Attendee> GetAttendeesForConference(Conference conference, int pageNumber, int perPage);
        Attendee GetAttendeeByEmail(string email);
    }
}
