using System;

namespace CodeCampServer.Model.Domain
{
    public class Speaker : IEquatable<Speaker>
    {
        private readonly Person _person;
        private readonly string _speakerKey;
        private readonly string _bio;
        private readonly string _avatarUrl;

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


        public bool Equals(Speaker speaker)
        {
            if (speaker == null) return false;
            if (!Equals(_person, speaker._person)) return false;
            if (!Equals(_speakerKey, speaker._speakerKey)) return false;
            if (!Equals(_bio, speaker._bio)) return false;
            if (!Equals(_avatarUrl, speaker._avatarUrl)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Speaker);
        }

        public override int GetHashCode()
        {
            int result = _person.GetHashCode();
            result = 29*result + _speakerKey.GetHashCode();
            result = 29*result + _bio.GetHashCode();
            result = 29*result + _avatarUrl.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return GetName();
        }
    }
}