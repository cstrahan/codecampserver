using System;

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

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Abstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }

        public void SetSpeaker(Speaker value)
        {
            _speaker = value;
        }

        public Speaker GetSpeaker()
        {
            return _speaker;
        }
    }
}