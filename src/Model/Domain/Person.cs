using System;

namespace CodeCampServer.Model.Domain
{
    public class Person : EntityBase
    {
        private readonly Contact _contact = new Contact();
        private string _comment;
        private string _password;
        private string _passwordSalt;
        private string _website;
        private bool _isAdministrator;
        private Conference _conference;

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

        public virtual bool IsAdministrator
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
            Speaker speaker = Array.Find(speakers,
                                         delegate(Speaker aSpeaker) { return this.Equals(aSpeaker.Person); });
            return speaker;
        }

        public override string ToString()
        {
            return Contact.FullName;
        }

        public virtual bool Equals(Person person)
        {
            if (person == null) return false; 
            if (!Equals(_contact, person.Contact)) return false;
            if (!Equals(_comment, person._comment)) return false;
            if (!Equals(_password, person._password)) return false;
            if (!Equals(_passwordSalt, person._passwordSalt)) return false;
            if (!Equals(_website, person._website)) return false;
            if (!Equals(_isAdministrator, person._isAdministrator)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Person);
        }

        public override int GetHashCode()
        {
            var result = 14;
            result = 29*result + (_contact.GetHashCode());
            result = 29*result + (_comment != null ? _comment.GetHashCode() : 0);
            result = 29*result + (_password != null ? _password.GetHashCode() : 0);
            result = 29*result + (_passwordSalt != null ? _passwordSalt.GetHashCode() : 0);
            result = 29*result + (_website != null ? _website.GetHashCode() : 0);
            result = 29*result + _isAdministrator.GetHashCode();
            return result;
        }
    }
}