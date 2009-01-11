using System;
using CodeCampServer.UI.Models.Forms.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class TimeSlotForm
	{
		public virtual Guid Id { get; set; }
		public virtual string StartTime { get; set; }
		public virtual string EndTime { get; set; }
		public virtual Guid ConferenceId { get; set; }

		[Hidden]
		public virtual string ConferenceKey { get; set; }
	}
}