using System;
using System.ComponentModel.DataAnnotations;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Models.Input
{
	public class MeetingInput : EventInput,IMessage
	{
		public Guid Id { get; set; }
		public Guid UserGroupId { get; set; }

		[Required()]
		public string Name { get; set; }

		[Required()]
		public string Topic { get; set; }

		[Required()]
		[Multiline]
		public string Summary { get; set; }

		[Required()]
		[RegularExpression(@"^[A-Za-z0-9\-]+$",ErrorMessage = "Key should only contain letters, numbers, and hypens.")]
		public string Key { get; set; }

		[Required()]
		public override DateTime StartDate { get; set; }

		[Required()]
		public override DateTime EndDate { get; set; }

		[Required()]
		public override string TimeZone { get; set; }

		[Multiline]
		public string Description { get; set; }

		[Required()]
		public string LocationName { get; set; }

		public string LocationUrl { get; set; }

		[Required()]
		public string SpeakerName { get; set; }

		[Label("Speaker Website")]
		public string SpeakerUrl { get; set; }

		[Required()]
		[Multiline]
		[Label("Bio")]
		public string SpeakerBio { get; set; }
	}
}