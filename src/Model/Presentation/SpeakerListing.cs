using CodeCampServer.Model.Domain;
using System.Collections.Generic;
using System.Collections;

namespace CodeCampServer.Model.Presentation
{
	public class SpeakerListing
	{
		private Speaker _speaker;

        public SpeakerListing(Speaker speaker)
		{
            _speaker = speaker;
		}

	    public string Key
	    {
            get { return _speaker.DisplayName; }
	    }

		public string Name
		{
            get { return _speaker.GetName(); }
		}

		public string DisplayName
		{
            get { return _speaker.DisplayName; }
		}

    }

	//TODO:  Put in it's own file
    public class SpeakerListingCollection : IEnumerable<SpeakerListing>
    {
        private List<SpeakerListing> _list;

        public SpeakerListingCollection(IEnumerable<Speaker> speakers)
        {
            _list = new List<SpeakerListing>();
            foreach(var speaker in speakers)
                _list.Add(new SpeakerListing(speaker));
        }

        public IEnumerator<SpeakerListing> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

    }
}
