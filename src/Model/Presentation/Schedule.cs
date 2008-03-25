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
        private readonly ITrackRepository _trackRepository;

        public Schedule(Conference conference, IClock clock, ITimeSlotRepository timeSlotRepository, ITrackRepository trackRepository)
        {
            _conference = conference;
            _clock = clock;
            _timeSlotRepository = timeSlotRepository;
            _trackRepository = trackRepository;
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
            ScheduleListing[] scheduleListings = getScheduleListingsFromTimeSlots(timeSlots);
            return scheduleListings;
        }

        public TrackListing[] GetTrackListings()
        {
            Track[] tracks = _trackRepository.GetTracksFor(_conference);
            TrackListing[] trackListings = getTrackListingsFromTracks(tracks);
            return trackListings;
        }

        private static ScheduleListing[] getScheduleListingsFromTimeSlots(IEnumerable<TimeSlot> timeSlots)
        {
            var listings = new List<ScheduleListing>();
            foreach (TimeSlot timeSlot in timeSlots)
            {
                listings.Add(new ScheduleListing(timeSlot));
            }
            return listings.ToArray();
        }

        private static TrackListing[] getTrackListingsFromTracks(IEnumerable<Track> tracks)
        {
            var listings = new List<TrackListing>();
            foreach (Track track in tracks)
            {
                listings.Add(new TrackListing(track));
            }
            return listings.ToArray();
        }
    }
}