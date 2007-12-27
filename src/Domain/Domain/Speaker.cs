namespace CodeCampServer.Model.Domain
{
    public class Speaker : Attendee
    {
        private string _avatarUrl;

        public virtual string AvatarUrl
        {
            get { return _avatarUrl; }
            set { _avatarUrl = value; }
        }
    }
}