namespace CodeCampServer.Domain.Model
{
    public class Sponsor : EntityBase
    {
        private string _name;
        private Contact _contact = new Contact();
        private string _website;
        private string _logoUrl;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        public string LogoUrl
        {
            get { return _logoUrl; }
            set { _logoUrl = value; }
        }
    }
}