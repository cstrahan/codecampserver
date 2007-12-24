using System;

namespace CodeCampServer.Domain.Model
{
    public class TimeSlot
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private Session _session;

        public TimeSlot()
        {
        }

        public TimeSlot(DateTime startTime, DateTime endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
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

        public virtual void SetSession(Session value)
        {
            _session = value;
        }

        public virtual Session GetSession()
        {
            return _session;
        }
    }
}