namespace CodeCampServer.Core.Domain.Model
{
	public class Meeting : Event
	{
		public virtual string Topic { get; set; }
		public virtual string Summary { get; set; }
		public virtual string SpeakerName { get; set; }
		public virtual string SpeakerBio { get; set; }
		public virtual string SpeakerUrl { get; set; }

		public override string Title()
		{
			return Name + ": " + Topic;
		}
	}
}