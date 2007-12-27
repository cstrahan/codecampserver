namespace CodeCampServer.Model.Domain
{
    public class Attendee : EntityBase
    {
        private Contact _contact = new Contact();
        private string _website;
        private string _comment;
        private Conference _conference;

        public Attendee()
        {
        }

        public Attendee(string firstName, string lastName, string website,
                        string comment, Conference conference, string email)
        {
            _contact.FirstName = firstName;
            _contact.LastName = lastName;
            _contact.Email = email;
            _website = website;
            _comment = comment;
            _conference = conference;
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

        public virtual Conference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }
    }
}