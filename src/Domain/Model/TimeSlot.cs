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

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public void SetSession(Session value)
        {
            _session = value;
        }

        public Session GetSession()
        {
            return _session;
        }
    }
}