using Iesi.Collections.Generic;
using System.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
	public class Session : EntityBase
	{
		private Speaker _speaker;
		private string _title;
		private string _abstract;
        private bool _isApproved;
        private ISet<OnlineResource> _onlineResources = new HashedSet<OnlineResource>();
        private Track _track;
        private TimeSlot _timeSlot;

		public Session()
		{
		}

        public Session(Speaker speaker, string title, string @abstract)
        {
            _speaker = speaker;
            _title = title;
            _abstract = @abstract;
        }

		public Session(Speaker speaker, string title, string @abstract, Track track)
		{
			_speaker = speaker;
			_title = title;
			_abstract = @abstract;
			_track = track;
		}

		public Session(Speaker speaker, string title, string @abstract, Track track, OnlineResource[] onlineResources)
        {
            _speaker = speaker;
            _title = title;
            _abstract = @abstract;
            _onlineResources = new HashedSet<OnlineResource>(onlineResources);
            _track = track;
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

	    public virtual bool IsApproved
	    {
            get { return _isApproved; }
            set { _isApproved = value; }
	    }

        public virtual OnlineResource[] GetResources()
        {
            return new List<OnlineResource>(_onlineResources).ToArray();
        }

        public virtual void AddResource(OnlineResource resource)
        {
            _onlineResources.Add(resource);
        }

        public virtual Track Track
        {
            get { return _track; }
            set { _track = value; }
        }

        public virtual TimeSlot TimeSlot
        {
            get { return _timeSlot; }
            set { _timeSlot = value; }
        }
    }
}
