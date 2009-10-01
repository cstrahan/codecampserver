namespace CodeCampServer.UI.Models.Input
{
	public class MeetingAnnouncementDisplay
	{
		public string Heading { get; set; }
		public string Topic { get; set; }
		public string Summary { get; set; }
		public DateTimeSpan When { get; set; }
		public string LocationName { get; set; }
		public string LocationUrl { get; set; }
		public string SpeakerName { get; set; }
		public string SpeakerUrl { get; set; }
		public string SpeakerBio { get; set; }
		public string MeetingInfo { get; set; }
	}
}