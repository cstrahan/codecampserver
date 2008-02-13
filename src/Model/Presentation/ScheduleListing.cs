using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
	public class ScheduleListing
	{
		private readonly TimeSlot _timeSlot;

		public ScheduleListing(TimeSlot timeSlot)
		{
			_timeSlot = timeSlot;
		}

		public string StartTime
		{
			get { return _timeSlot.StartTime.ToShortTimeString(); }
		}

		public string EndTime
		{
			get { return _timeSlot.EndTime.ToShortTimeString(); }
		}

		public string Purpose
		{
			get { return _timeSlot.Purpose; }
		}

		public SessionListing[] Sessions
		{
			get
			{
				List<SessionListing> sessions = new List<SessionListing>();
				foreach (Session session in _timeSlot.GetSessions())
				{
					sessions.Add(new SessionListing(session));
				}
				return sessions.ToArray();
			}
		}
	}
}