using System;
using System.Collections.Generic;
using System.Text;
using CodeCampServer.Model.Domain;
using System.Collections;

namespace CodeCampServer.Model.Presentation
{
    public class SpeakerListingCollection : IEnumerable<SpeakerListing>
    {
        private List<SpeakerListing> _list;

        public SpeakerListingCollection(IEnumerable<Speaker> speakers)
        {
            _list = new List<SpeakerListing>();
            foreach (var speaker in speakers)
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
