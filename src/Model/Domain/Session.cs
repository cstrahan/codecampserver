namespace CodeCampServer.Model.Domain
{
    public class Session : EntityBase
    {
        private Conference _conference;
        private Person _speaker;
        private string _title;
        private string _abstract;
        private bool _isApproved;
        private Track _track;
        private TimeSlot _timeSlot;

        public Session()
        {
        }

        public Session(Conference conference, Person speaker, string title, string @abstract)
        {
            _conference = conference;
            _speaker = speaker;
            _title = title;
            _abstract = @abstract;
        }

        public Session(Conference conference, Person speaker, string title, string @abstract, Track track)
            :this(conference, speaker, title, @abstract)
        {
            _track = track;
        }

        public virtual Conference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }

        public virtual string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public virtual string Abstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }

        public virtual Person Speaker
        {
            get { return _speaker; }
            set { _speaker = value; }
        }

        public virtual bool IsApproved
        {
            get { return _isApproved; }
            set { _isApproved = value; }
        }

        public virtual Track Track
        {
            get { return _track; }
            set { _track = value; }
        }

        public virtual TimeSlot TimeSlot
        {
            get { return _timeSlot; }
            set { _timeSlot = value; }
        }

        public virtual Speaker GetSpeakerProfile()
        {
            return Speaker.GetSpeakerProfileFor(Conference);
        }
    }
}