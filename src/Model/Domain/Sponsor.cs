using System;

namespace CodeCampServer.Model.Domain
{
    public class Sponsor : IEquatable<Sponsor>
    {
        private string _name;
        private Contact _contact = new Contact();
        private string _website;
        private string _logoUrl;

        public bool Equals(Sponsor sponsor)
        {
            if (sponsor == null) return false;
            return Equals(_name, sponsor._name) && Equals(_level, sponsor._level);
        }

        public override int GetHashCode()
        {
            return (_name != null ? _name.GetHashCode() : 0) + 29 * _level.GetHashCode();
        }

        private SponsorLevel _level;

        public Sponsor()
        {
        }

        public Sponsor(string name, string logoUrl, string website)
        {
            _name = name;
            _logoUrl = logoUrl;
            _website = website;
        }

        public Sponsor(string name, string logoUrl, string website, SponsorLevel level)
            : this(name, logoUrl, website)
        {
            _level = level;
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
    }
}