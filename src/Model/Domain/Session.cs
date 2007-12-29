using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
    public class Session : EntityBase
    {
        private string _abstract;
        private string _title;
        private Speaker _speaker;
        private ISet<OnlineResource> _onlineResources = new HashedSet<OnlineResource>();

        public Session()
        {
        }

        public Session(Speaker speaker, string title)
        {
            _speaker = speaker;
            _title = title;
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

        public virtual Speaker Speaker
        {
            get { return _speaker; }
            set { _speaker = value; }
        }

        public virtual OnlineResource[] Resources
        {
            get { return new List<OnlineResource>(_onlineResources).ToArray(); }
        }

        public virtual void AddResource(OnlineResource resource)
        {
            _onlineResources.Add(resource);
        }
    }
}