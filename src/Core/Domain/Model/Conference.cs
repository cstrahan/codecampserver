using System;
using System.Linq;
using Iesi.Collections.Generic;

namespace CodeCampServer.Core.Domain.Model
{
	public class Conference : KeyedObject
	{
		private readonly ISet<Attendee> _attendees = new HashedSet<Attendee>();
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
	}
}