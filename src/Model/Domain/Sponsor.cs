namespace CodeCampServer.Model.Domain
{
    public class Sponsor : EntityBase
    {
        private string _name;
        private Contact _contact = new Contact();
        private string _website;
        private string _logoUrl;

        public Sponsor()
        {
        }

        public Sponsor(string name, string logoUrl, string website)
        {
            _name = name;
            _logoUrl = logoUrl;
            _website = website;
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
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

        public virtual string LogoUrl
        {
            get { return _logoUrl; }
            set { _logoUrl = value; }
        }
    }
}
