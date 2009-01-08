using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCampServer.Core.Domain.Model
{
    public class Conference : KeyedObject
    {
        private readonly IList<Attendee> _attendees = new List<Attendee>();
        public virtual string ConferenceKey { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string LocationName { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string PhoneNumber { get; set; }

        public virtual Attendee[] Attendees
        {
            get { return _attendees.ToArray(); }
        }

        public virtual void AddAttendee(Attendee attendee)
        {
            _attendees.Add(attendee);
        }

        public virtual void RemoveAttendee(Attendee attendee)
        {
            _attendees.Remove(attendee);
        }
    }
}
