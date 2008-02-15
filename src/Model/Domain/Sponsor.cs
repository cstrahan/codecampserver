using System;

namespace CodeCampServer.Model.Domain
{
    public class Sponsor : IEquatable<Sponsor>
    {
        private string _name = string.Empty;
        private string _website = string.Empty;
        private string _logoUrl = string.Empty;
        private Contact _contact = new Contact();
        private SponsorLevel _level;

        public Sponsor(string name, string logoUrl, string website,
                       string firstName, string lastName, string email,
                       SponsorLevel level)
        {
            _level = level;
            _name = name;
            _logoUrl = logoUrl;
            _website = website;
            _contact.FirstName = firstName;
            _contact.LastName = lastName;
            _contact.Email = email;
        }

        public Sponsor()
        {
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual Contact Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        public virtual string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        public virtual string LogoUrl
        {
            get { return _logoUrl; }
            set { _logoUrl = value; }
        }

        public virtual SponsorLevel Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public bool Equals(Sponsor sponsor)
        {
            if (sponsor == null) return false;
            return Equals(_name, sponsor._name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Sponsor);
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}
