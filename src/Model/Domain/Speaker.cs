namespace CodeCampServer.Model.Domain
{
    public class Speaker : Role
    {
        private string _avatarUrl;
    	private string _profile;
        private string _displayName;

        public Speaker()
        {
        }

        public Speaker(string firstName, string lastName, string url, string comment, Conference conference, string email, string displayName, string avatarUrl, string profile, string password, string passwordSalt)
        {
            Person.Contact.FirstName = firstName;
            Person.Contact.LastName = lastName;
            Person.Contact.Email = email;
            Person.Comment = comment;
            Person.Conference = conference;
            Person.Website = url;
            Person.Password = password;
            Person.PasswordSalt = passwordSalt;

            _displayName = displayName;
            _avatarUrl = avatarUrl;
        	_profile = profile;
        }

        public virtual string AvatarUrl
        {
            get { return _avatarUrl; }
            set { _avatarUrl = value; }
        }

		public virtual string Profile{
			get { return _profile; }
			set { _profile = value;}
		}

        public virtual string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
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
