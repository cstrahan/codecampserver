using System;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
    public class TrackListing : IEquatable<TrackListing>
    {
        private readonly Track _track;

        public TrackListing(Track track)
        {
            _track = track;
        }

        public string Name
        {
            get { return _track.Name; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TrackListing);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(TrackListing other)
        {
            return (other != null) && String.Equals(Name, other.Name);
        }
    }
}
