namespace CodeCampServer.Model.Domain
{
    public class Speaker : Attendee
    {
        private string _avatarUrl;


        public Speaker()
        {
        }

        public Speaker(string firstName, string lastName, string url, string comment, Conference conference, string email, string avatarUrl)
            : base(firstName, lastName, url, comment, conference, email)
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