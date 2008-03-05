namespace CodeCampServer.Model.Domain
{
    public class Attendee : Role
    {
        public Attendee()
        {
        }

        public Attendee(string firstName, string lastName, string website,
                        string comment, Conference conference, string email, string password, string passwordSalt)
        {
            Person.Contact.FirstName = firstName;
            Person.Contact.LastName = lastName;
            Person.Contact.Email = email;
            Person.Website = website;
            Person.Comment = comment;
            Person.Conference = conference;
            Person.Password = password;
            Person.PasswordSalt = passwordSalt;
        }

    	public Attendee(string firstName, string lastName)
    	{
            Person.Contact.FirstName = firstName;
            Person.Contact.LastName = lastName;
    	}

    	public virtual Contact Contact
        {
            get { return Person.Contact; }
        }

        public virtual string Website
        {
            get { return Person.Website; }
            set { Person.Website = value; }
        }

        public virtual string Comment
        {
            get { return Person.Comment; }
            set { Person.Comment = value; }
        }

        public virtual string Password
        {
            get { return Person.Password; }
            set { Person.Password = value; }
        }

        public virtual string PasswordSalt
        {
            get { return Person.PasswordSalt; }
            set { Person.PasswordSalt = value; }
        }

        public virtual Conference Conference
        {
            get { return Person.Conference; }
            set { Person.Conference = value; }
        }

        public virtual string GetName()
        {
            return Person.GetName();
        }
    }
}
