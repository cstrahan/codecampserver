using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
	public class SpeakerListing
	{
		private readonly Speaker _speaker;

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