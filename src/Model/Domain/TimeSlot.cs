using System;
using System.Linq;
using Iesi.Collections.Generic;
using System.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    public class TimeSlot : EntityBase
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private string _purpose;
        private Conference _conference;
        private ISet<Session> _sessions = new HashedSet<Session>();

        public TimeSlot()
        {
        }

        public TimeSlot(DateTime startTime, DateTime endTime, string purpose)
        {
            _startTime = startTime;
            _endTime = endTime;
            _purpose = purpose;
        }

        public TimeSlot(Conference conference, DateTime startTime, DateTime endTime, string purpose)
        {
            _conference = conference;
            _startTime = startTime;
            _endTime = endTime;
            _purpose = purpose;
        }

        public virtual DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public virtual DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public virtual string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }

        public virtual Conference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }

        public virtual void AddSession(Session session)
        {
            Session sessionInSameTrack = _sessions.FirstOrDefault(s => s.Track == session.Track);

            if(sessionInSameTrack != null)
                throw new InvalidOperationException("Cannot have two sessions in the same track at the same time.");

            _sessions.Add(session);
        }
        
        public virtual void RemoveSession(Session session)
        {
            _sessions.Remove(session);
        }

    	public virtual Session[] GetSessions()
    	{
    		return new List<Session>(_sessions).ToArray();
    	}
    }
}
