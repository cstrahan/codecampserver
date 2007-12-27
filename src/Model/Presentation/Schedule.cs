using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
    public class Schedule
    {
        private readonly Conference _conference;

        public Schedule(Conference conference)
        {
            _conference = conference;
        }

        public IEnumerable<ScheduleListing> GetListings()
        {
            foreach (TimeSlot slot in _conference.TimeSlots)
            {
                yield return new ScheduleListing(slot);
            }
        }
    }
}