using System.Collections.Generic;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
	public class ScheduleListing
	{
		private readonly TimeSlot _timeSlot;

		private readonly IDictionary<TrackListing, SessionListing> _sessionsByTrack =
			new Dictionary<TrackListing, SessionListing>();

		public ScheduleListing(TimeSlot timeSlot)
		{
			_timeSlot = timeSlot;

			// Store the set of sessions keyed by track, for easy lookup
			Session[] sessions = _timeSlot.GetSessions();
			foreach (Session session in sessions)
			{
				_sessionsByTrack.Add(new TrackListing(session.Track), new SessionListing(session));
			}
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
			get { return new List<SessionListing>(_sessionsByTrack.Values).ToArray(); }
		}

		public SessionListing this[TrackListing trackListing]
		{
			get
			{
				// Since it's acceptable that not every track have an associated session,
				// return null here instead of throwing
				return _sessionsByTrack.ContainsKey(trackListing) ? _sessionsByTrack[trackListing] : null;
			}
		}
	}
}