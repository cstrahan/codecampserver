namespace CodeCampServer.Model.Domain
{
    public class Speaker : Attendee
    {
        private string _avatarUrl;


        public Speaker()
        {
        }

        public Speaker(string firstName, string lastName, string url, string comment, Conference conference, string email, string avatarUrl, string password, string passwordSalt)
            : base(firstName, lastName, url, comment, conference, email, password, passwordSalt)
        {
            _avatarUrl = avatarUrl;
        }

        public virtual string AvatarUrl
        {
            get { return _avatarUrl; }
            set { _avatarUrl = value; }
        }
    }
}
