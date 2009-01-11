using System;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface ITimeSlotMessage
	{
		Guid Id { get; set; }
		string StartTime { get; set; }
		string EndTime { get; set; }
		Guid ConferenceId { get; set; }
	}
}