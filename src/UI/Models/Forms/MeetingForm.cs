using System;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class MeetingForm : EventForm
	{
		public Guid Id { get; set; }
		public Guid UserGroupId { get; set; }

		[Required("Name")]
		public string Name { get; set; }

		[Required("Topic")]
		public string Topic { get; set; }

		[Required("Summary")]
		[Multiline]
		public string Summary { get; set; }

		[Required("Event Key")]
		[ValidateRegExp(@"^[A-Za-z0-9\-]+$", "Key should only contain letters, numbers, and hypens.")]
		public string Key { get; set; }

		[RequiredDateTime("Start Date")]
		public override DateTime StartDate { get; set; }

		[RequiredDateTime("End Date")]
		public override DateTime EndDate { get; set; }

		[Required("Time Zone")]
		public override string TimeZone { get; set; }

		public string Description { get; set; }

		[Required("Location")]
		public string LocationName { get; set; }

		public string LocationUrl { get; set; }

		[Required("Speaker")]
		public string SpeakerName { get; set; }

		[Label("Speaker Website")]
		public string SpeakerUrl { get; set; }

		[Required("Bio")]
		[Multiline]
		public string SpeakerBio { get; set; }
	}
}