using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Presentation
{
	public class SessionListing
	{
		private readonly Session _session;
		private readonly SpeakerListing _speaker;
		private readonly TrackListing _track;

		public SessionListing(Session session)
		{
			_session = session;
			_speaker = new SpeakerListing(_session.GetSpeakerProfile());
			_track = new TrackListing(_session.Track);
		}

		public string Title
		{
			get { return _session.Title; }
		}

		public string Abstract
		{
			get { return _session.Abstract; }
		}

		public SpeakerListing Speaker
		{
			get { return _speaker; }
		}

		public TrackListing Track
		{
			get { return _track; }
		}
	}
}