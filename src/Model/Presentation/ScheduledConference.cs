using System;
using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
    public class ScheduledConference
    {
        private readonly Conference _conference;
    	private IClock _clock;

        public ScheduledConference(Conference conference, IClock clock)
        {
        	_conference = conference;
        	_clock = clock;
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
				int days = ((DateTime)_conference.StartDate - today).Days;
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

    	public IEnumerable<ScheduleListing> GetSchedule()
        {
            foreach (TimeSlot slot in _conference.TimeSlots)
            {
                yield return new ScheduleListing(slot);
            }
        }
    }
}