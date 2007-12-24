using System;

namespace CodeCampServer.Domain.Model
{
    public class Attendee
    {
        private Guid _id;
        private string _name;
        private string _website;
        private string _comment;
        private Conference _Conference;


        protected Attendee()
        {
        }

        public Attendee(string name, string website, string comment, Conference theConference)
        {
            _name = name;
            _website = website;
            _comment = comment;
            _Conference = theConference;
        }


        public virtual Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public virtual string Website
        {
            get { return _website; }
        }

        public virtual string Comment
        {
            get { return _comment; }
        }

        public virtual Conference Conference
        {
            get { return _Conference; }
        }
    }
}