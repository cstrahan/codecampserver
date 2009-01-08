using System;

namespace CodeCampServer.Core.Services.Updaters
{
	public interface ITimeSlotMessage
	{
		Guid Id { get; set; }
		string StartTime { get; set; }
		string EndTime { get; set; }
		Guid ConferenceId { get; set; }
	}
}