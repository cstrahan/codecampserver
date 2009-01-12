using System;
using CodeCampServer.Core;
using CodeCampServer.UI.Models.Forms.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class TimeSlotForm : ValueObject<TimeSlotForm>
	{
		public virtual Guid Id { get; set; }
		[BetterValidateDateTime("Start Time")]
		public virtual string StartTime { get; set; }
		[BetterValidateDateTime("End Time")]
		public virtual string EndTime { get; set; }
		public virtual Guid ConferenceId { get; set; }

		[Hidden]
		public virtual string ConferenceKey { get; set; }

		public string GetName()
		{
			return string.Format("{0}-{1}", StartTime, EndTime);
		}
	}
}