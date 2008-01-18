namespace CodeCampServer.Model.Domain
{
    public class Speaker : Attendee
    {
        private string _avatarUrl;
    	private string _profile;
        private string _displayName;

        public Speaker()
        {
        }

        public Speaker(string firstName, string lastName, string url, string comment, Conference conference, string email, string displayName, string avatarUrl, string profile, string password, string passwordSalt)
            : base(firstName, lastName, url, comment, conference, email, password, passwordSalt)
        {
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
    }
}
