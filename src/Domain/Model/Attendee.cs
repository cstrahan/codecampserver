using System;

namespace CodeCampServer.Domain.Model
{
    public class Attendee
    {
        private Guid _id;
        private string _name;
        private string _website;
        private string _comment;
        private Event _event;


        protected Attendee()
        {
        }

        public Attendee(string name, string website, string comment, Event theEvent)
        {
            _name = name;
            _website = website;
            _comment = comment;
            _event = theEvent;
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

        public virtual Event Event
        {
            get { return _event; }
        }
    }
}