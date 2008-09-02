using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    public class Conference : EntityBase
    {
        private string _key;
        private string _name;
        private string _description;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _sponsorInfo;
        private readonly Location _location = new Location();
        private int _maxAttendees;
        private readonly ISet<Sponsor> _sponsors = new HashedSet<Sponsor>();
        private readonly ISet<Person> _attendees = new HashedSet<Person>();
        private readonly ISet<Speaker> _speakers = new HashedSet<Speaker>();
        private bool _publiclyVisible;

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

        public virtual bool PubliclyVisible
        {
            get { return _publiclyVisible; }
            set { _publiclyVisible = value; }
        }

        //public virtual TimeSlot AddTimeSlot(DateTime startTime, DateTime endTime, string purpose)
        //{
        //    if(startTime < StartDate || endTime > EndDate)
        //    {
        //        throw new Exception("Time slot must be within conference.");
        //    }

        //    TimeSlot timeSlot = new TimeSlot(startTime, endTime, purpose);
        //    _timeSlots.Add(timeSlot);
        //    return timeSlot;
        //}

        //public virtual TimeSlot[] GetTimeSlots()
        //{
        //    return new List<TimeSlot>(_timeSlots).ToArray();
        //}

        //public virtual TimeSlot[] GetTimeSlots(DateTime date)
        //{
        //    DateTime day = date.Date;
        //    DateTime startDay = StartDate.Value.Date;
        //    DateTime endDay = EndDate.Value.Date;

        //    if (day < startDay || day > endDay)
        //    {
        //        throw new Exception("Date must be within conference.");
        //    }

        //    List<TimeSlot> timeSlots = new List<TimeSlot>(_timeSlots);
        //    return timeSlots.Where(t => t.StartTime.Date == day).ToArray();
        //}

        public virtual Sponsor[] GetSponsors()
        {
            Sponsor[] result = new List<Sponsor>(_sponsors).ToArray();
            Array.Sort(result, (x, y) => y.Level.CompareTo(x.Level));
            return result;
        }

        public virtual Sponsor[] GetSponsors(SponsorLevel level)
        {
            var sponsors = new List<Sponsor>(_sponsors);
            return sponsors.FindAll(item => item.Level == level).ToArray();
        }

        public virtual void AddSponsor(Sponsor sponsor)
        {
            _sponsors.Add(sponsor);
        }

        public virtual Sponsor GetSponsor(string sponsorName)
        {
            return new List<Sponsor>(_sponsors)
                .Find(sponsor => sponsor.Name.ToLower() == sponsorName.ToLower());
        }

        public virtual void RemoveSponsor(Sponsor sponsor)
        {
            _sponsors.Remove(sponsor);
        }

        public override string ToString()
        {
            return Key;
        }

        public virtual void AddAttendee(Person attendee)
        {
            _attendees.Add(attendee);
        }

        public virtual Person[] GetAttendees()
        {
            return new List<Person>(_attendees).ToArray();
        }

        public virtual void AddSpeaker(Person person, string speakerKey, string bio, string avatarUrl)
        {
            _speakers.Add(new Speaker(person, speakerKey, bio, avatarUrl));
        }

        public virtual Speaker[] GetSpeakers()
        {
            return new List<Speaker>(_speakers).ToArray();
        }

        public virtual Speaker GetSpeakerByKey(string id)
        {
            return Array.Find(GetSpeakers(), speaker => (speaker.SpeakerKey == id));
        }
    }
}
