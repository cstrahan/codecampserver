using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Domain
{
    public class Track : EntityBase
    {
        private string _name;
        private Conference _conference;

        public Track()
        {
        }

        public Track(string name)
        {
            _name = name;
        }

        public Track(Conference conference, string name)
        {
            _conference = conference;
            _name = name;
        }
    
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual Conference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }
    }
}
