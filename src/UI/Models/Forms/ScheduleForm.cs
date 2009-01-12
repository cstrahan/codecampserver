using CodeCampServer.Core;

namespace CodeCampServer.UI.Models.Forms
{
	public class ScheduleForm : ValueObject<ScheduleForm>
	{
		public TrackForm[] Tracks { get; set; }
		public TimeSlotAssignmentForm[] TimeSlotAssignments { get; set; }
	}

	public class TimeSlotAssignmentForm : ValueObject<TimeSlotAssignmentForm>
	{
		public TimeSlotForm TimeSlot { get; set; }
		public TrackAssignmentForm[] TrackAssignments { get; set; }
	}

	public class TrackAssignmentForm : ValueObject<TrackAssignmentForm>
	{
		public TrackForm Track { get; set; }
		public SessionForm[] Sessions { get; set; }
	}
}