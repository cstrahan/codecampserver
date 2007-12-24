using System;

namespace CodeCampServer.Domain.Model
{
    public class Attendee : EntityBase
    {
        private Guid _id;
        Contact _contact = new Contact();
        private string _website;
        private string _comment;
        private Conference _conference;

        public Attendee()
        {
        }

        public Attendee(string firstName, string lastName, string website, string comment, Conference conference, string email)
        {
            _contact.FirstName = firstName;
            _contact.LastName = lastName;
            _contact.Email = email;
            _website = website;
            _comment = comment;
            _conference = conference;
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Contact Contact
        {
            get { return _contact; }
        }

        public string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public Conference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }
    }
}