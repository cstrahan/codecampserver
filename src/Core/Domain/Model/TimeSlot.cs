using System;

namespace CodeCampServer.Core.Domain.Model
{
	public class TimeSlot : PersistentObject
	{
		public virtual DateTime? StartTime { get; set; }
		public virtual DateTime? EndTime { get; set; }
		public virtual Conference Conference { get; set; }
	}
}