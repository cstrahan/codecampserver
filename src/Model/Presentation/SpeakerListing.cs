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
            get { return _speaker.SpeakerKey; }
	    }

		public string DisplayName
		{
            get { return _speaker.GetName(); }
		}

    }

}
