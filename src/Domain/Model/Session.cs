namespace CodeCampServer.Domain.Model
{
    public class Session : EntityBase
    {
        private string _abstract;
        private string _title;
        private Speaker _speaker;

        public Session()
        {
        }

        public virtual string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public virtual string Abstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }

        public virtual void SetSpeaker(Speaker value)
        {
            _speaker = value;
        }

        public virtual Speaker GetSpeaker()
        {
            return _speaker;
        }
    }
}