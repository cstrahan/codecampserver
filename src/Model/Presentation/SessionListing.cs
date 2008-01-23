using CodeCampServer.Model.Domain;
using Iesi.Collections.Generic;
using System.Collections.Generic;

namespace CodeCampServer.Model.Presentation
{
    public class SessionListing
    {
        private Session _session;

        public SessionListing(Session session)
        {
            _session = session;
        }

        public string Title
        {
            get { return _session.Title; }
            set { _session.Title = value; }
        }

        public string Abstract
        {
            get { return _session.Abstract; }
            set { _session.Abstract = value; }
        }

        public SpeakerListing Speaker
        {
            get { return new SpeakerListing(_session.Speaker); }
        }

        public OnlineResource[] GetResources()
        {
            return new List<OnlineResource>(_session.GetResources()).ToArray();
        }
    }
}
