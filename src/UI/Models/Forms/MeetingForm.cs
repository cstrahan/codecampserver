using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class MeetingForm : EventForm
	{
		[BetterValidateNonEmpty("Topic")]
		public virtual string Topic { get; set; }

		[BetterValidateNonEmpty("Summary")]
		public virtual string Summary { get; set; }

		[BetterValidateNonEmpty("Speaker")]
		public virtual string SpeakerName { get; set; }

		[BetterValidateNonEmpty("Bio")]
		public virtual string SpeakerBio { get; set; }

		[Label("Speaker Website")]
		public virtual string SpeakerUrl { get; set; }
	}
}