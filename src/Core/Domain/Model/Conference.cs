using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCampServer.Core.Domain.Model
{
	public class Conference : KeyedObject
	{
		private readonly IList<Attendee> _attendees = new List<Attendee>();
		private readonly IList<Sponsor> _sponsors = new List<Sponsor>();
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
		public virtual string HtmlContent { get; set; }
		public virtual UserGroup UserGroup { get; set; }
        public virtual bool HasRegistration { get; set; }

		public virtual Sponsor[] GetSponsors()
		{
			return _sponsors.ToArray();
		}

		public virtual void AddAttendee(Attendee attendee)
		{
			_attendees.Add(attendee);
		}

		public virtual void RemoveAttendee(Attendee attendee)
		{
			_attendees.Remove(attendee);
		}

		public virtual Attendee GetAttendee(Guid attendeeId)
		{
			return _attendees.SingleOrDefault(x => x.Id == attendeeId);
		}

		public virtual Attendee[] GetAttendees()
		{
			return _attendees.ToArray();
		}

		public virtual bool IsAttending(Guid attendeeId)
		{
			return GetAttendee(attendeeId) != null;
		}

		public virtual bool IsAttending(string attendeeEmailAddress)
		{
			return _attendees.Count(x => x.EmailAddress == attendeeEmailAddress) > 0;
		}
	}
}