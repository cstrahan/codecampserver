using System;

namespace CodeCampServer.Domain.Model
{
    public class TimeSlot
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private Session _session;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
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