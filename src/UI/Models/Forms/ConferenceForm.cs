using CodeCampServer.UI.Helpers.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class ConferenceForm : EventForm
	{
		[Label("Phone Number")]
		public virtual string PhoneNumber { get; set; }

		public virtual bool HasRegistration { get; set; }

		public string HtmlContent { get; set; }
	}
}