using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
    [PluginFamily("Default")]
    public interface IAttendeeRepository
    {
        IEnumerable<Attendee> GetAttendeesForConference(Conference anConference);
        void SaveAttendee(Attendee attendee);
    }
}