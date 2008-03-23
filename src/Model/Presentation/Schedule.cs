using System;
using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
    public class Schedule
    {
        private readonly Conference _conference;
        private readonly IClock _clock;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public Schedule(Conference conference, IClock clock, ITimeSlotRepository timeSlotRepository)
        {
            _conference = conference;
            _clock = clock;
            _timeSlotRepository = timeSlotRepository;
        }

        public string Name
        {
            get { return _conference.Name; }
        }

        public string Key
        {
            get { return _conference.Key; }
        }

        public virtual int? DaysUntilStart
        {
            get
            {
                if (!_conference.StartDate.HasValue) return null;
                DateTime today = _clock.GetCurrentTime();
                int days = ((DateTime) _conference.StartDate - today).Days;
                if (days < 0) return null;
                return days;
            }
        }

        public DateTime StartDate
        {
            get { return _conference.StartDate.GetValueOrDefault(); }
        }

        public Conference Conference
        {
            get { return _conference; }
        }

        public ScheduleListing[] GetScheduleListings()
        {
            TimeSlot[] timeSlots = _timeSlotRepository.GetTimeSlotsFor(_conference);
            ScheduleListing[] listings = getListingsFromTimeSlots(timeSlots);
            return listings;
        }

        private static ScheduleListing[] getListingsFromTimeSlots(IEnumerable<TimeSlot> timeSlots)
        {
            var listings = new List<ScheduleListing>();
            foreach (TimeSlot timeSlot in timeSlots)
            {
                listings.Add(new ScheduleListing(timeSlot));
            }
            return listings.ToArray();
        }
    }
}