using System;
using CodeCampServer.UI.Models.Forms.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class TimeSlotForm
	{
		public virtual Guid Id { get; set; }
		[BetterValidateDateTime("Start Time")]
		public virtual string StartTime { get; set; }
		[BetterValidateDateTime("End Time")]
		public virtual string EndTime { get; set; }
		public virtual Guid ConferenceId { get; set; }

		[Hidden]
		public virtual string ConferenceKey { get; set; }
	}
}