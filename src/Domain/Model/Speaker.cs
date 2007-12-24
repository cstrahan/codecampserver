namespace CodeCampServer.Domain.Model
{
    public class Speaker : Attendee
    {
        private string _avatarUrl;

        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set { _avatarUrl = value; }
        }
    }
}