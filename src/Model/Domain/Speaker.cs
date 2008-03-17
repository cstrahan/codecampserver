namespace CodeCampServer.Model.Domain
{
    public class Speaker
    {
        private Person _person;
        private string _speakerKey;
        private string _bio;
        private string _avatarUrl;

        public Speaker()
        {
        }


        public Speaker(Person person, string speakerKey, string bio, string avatarUrl)
        {
            _person = person;
            _speakerKey = speakerKey;
            _bio = bio;
            _avatarUrl = avatarUrl;
        }

        public Person Person
        {
            get { return _person; }
        }

        public virtual string AvatarUrl
        {
            get { return _avatarUrl; }
        }

        public virtual string Bio
        {
            get { return _bio; }
        }

        public virtual string SpeakerKey
        {
            get { return _speakerKey; }
        }

        public virtual string GetName()
        {
            return Person.GetName();
        }
    }
}