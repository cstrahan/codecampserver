using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace CodeCampServer.Domain.Model
{
    public class Conference : EntityBase
    {
        private string _key;
        private string _name;
        private string _description;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _sponsorInfo;
        private Location _location = new Location();
        private int _maxAttendees;
        private ISet<TimeSlot> _timeSlots = new HashedSet<TimeSlot>();
        private ISet<Sponsor> _sponsors = new HashedSet<Sponsor>();

        public Conference()
        {
        }

        public Conference(string key, string name)
        {
            _key = key;
            _name = name;
        }

        public virtual string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public virtual string SponsorInfo
        {
            get { return _sponsorInfo; }
            set { _sponsorInfo = value; }
        }

        public virtual Location Location
        {
            get { return _location; }
        }

        public virtual int MaxAttendees
        {
            get { return _maxAttendees; }
            set { _maxAttendees = value; }
        }

        public virtual void AddTimeSlot(DateTime startTime, DateTime endTime)
        {
            if(startTime < StartDate || endTime > EndDate)
            {
                throw new Exception("Time slot must be within conference.");
            }

            _timeSlots.Add(new TimeSlot(startTime, endTime));
        }

        public virtual TimeSlot[] GetTimeSlots()
        {
            return new List<TimeSlot>(_timeSlots).ToArray();
        }

        public virtual Sponsor[] GetSponsors()
        {
            return new List<Sponsor>(_sponsors).ToArray();
        }

        public virtual void AddSponsor(Sponsor sponsor)
        {
            _sponsors.Add(sponsor);
        }
    }
}