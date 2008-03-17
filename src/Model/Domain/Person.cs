using System;
using System.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    public class Person : EntityBase
    {
        private readonly Contact _contact = new Contact();
        private string _comment;
        private Conference _conference;
        private string _password;
        private string _passwordSalt;
        private string _website;
        private bool _isAdministrator;

        

        public Person()
        {
        }

        public Person(string firstName, string lastName, string email)
        {
            _contact.FirstName = firstName;
            _contact.LastName = lastName;
            _contact.Email = email;
        }

        public virtual Contact Contact
        {
            get { return _contact; }
        }

        public virtual string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public virtual string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public virtual string PasswordSalt
        {
            get { return _passwordSalt; }
            set { _passwordSalt = value; }
        }

        public virtual Conference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }

        public bool IsAdministrator
        {
            get { return _isAdministrator; }
            set { _isAdministrator = value; }
        }

        public virtual string GetName()
        {
            Contact contact = Contact;
            return string.Format("{0} {1}", contact.FirstName, contact.LastName);
        }

        public virtual Speaker GetSpeakerProfileFor(Conference conference)
        {
            Speaker[] speakers = conference.GetSpeakers();
            Speaker speaker = Array.Find(speakers, delegate(Speaker aSpeaker) { return aSpeaker.Person == this; });
            return speaker;
        }
    }
}