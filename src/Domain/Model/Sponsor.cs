namespace CodeCampServer.Domain.Model
{
    public class Sponsor
    {
        private string _name;
        private string _contactName;
        private string _contactEmail;
        private string _website;
        private string _logoUrl;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ContactName
        {
            get { return _contactName; }
            set { _contactName = value; }
        }

        public string ContactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; }
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