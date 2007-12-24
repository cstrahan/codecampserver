using System.Collections.Generic;
using CodeCampServer.Domain.Model;
using StructureMap;

namespace CodeCampServer.Domain
{
    [PluginFamily("Default")]
    public interface IAttendeeRepository
    {
        IEnumerable<Attendee> GetAttendeesForEvent(Conference anConference);
        void SaveAttendee(Attendee attendee);
    }
}