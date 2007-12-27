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

        public string SessionTitle
        {
            get { return _timeSlot.Session.Title.Trim(); }
        }

        public string SpeakerName
        {
            get { return getSpeakerName(); }
        }

        private string getSpeakerName()
        {
            Contact contact = _timeSlot.Session.Speaker.Contact;
            return string.Format("{0} {1}", contact.FirstName, contact.LastName);
        }
    }
}