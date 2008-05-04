using System;

namespace CodeCampServer.Model.Domain
{
    public class Contact : IEquatable<Contact>
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;

        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual string FullName
        {
            get { return _firstName + " " + _lastName; }
        }

        public virtual bool Equals(Contact contact)
        {
            if (contact == null) return false;
            if (!Equals(_firstName, contact._firstName)) return false;
            if (!Equals(_lastName, contact._lastName)) return false;
            if (!Equals(_email, contact._email)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Contact);
        }

        public override int GetHashCode()
        {
            int result = _firstName != null ? _firstName.GetHashCode() : 0;
            result = 29*result + (_lastName != null ? _lastName.GetHashCode() : 0);
            result = 29*result + (_email != null ? _email.GetHashCode() : 0);
            return result;
        }
    }
}
