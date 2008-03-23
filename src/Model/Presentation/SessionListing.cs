using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
    public class SessionListing
    {
        private readonly Session _session;
        private readonly SpeakerListing _listing;

        public SessionListing(Session session)
        {
            _session = session;
            _listing = new SpeakerListing(_session.GetSpeakerProfile());
        }

        public string Title
        {
            get { return _session.Title; }
        }

        public string Abstract
        {
            get { return _session.Abstract; }
        }

        public SpeakerListing Listing
        {
            get { return _listing; }
        }
    }
}
