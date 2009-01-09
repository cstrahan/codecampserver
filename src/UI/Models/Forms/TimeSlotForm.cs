using System;
using CodeCampServer.Core.Services.Updaters;
using CodeCampServer.UI.Models.Forms.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class TimeSlotForm : ITimeSlotMessage
	{
		public Guid Id { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public Guid ConferenceId { get; set; }
		[Hidden]
		public string ConferenceKey { get; set; }
					  
	}
}