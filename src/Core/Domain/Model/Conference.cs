using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCampServer.Core.Domain.Model
{
	public class Conference : Event
	{
		private readonly IList<Attendee> _attendees = new List<Attendee>();
		public virtual string PhoneNumber { get; set; }
		public virtual string HtmlContent { get; set; }
		public virtual bool HasRegistration { get; set; }

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