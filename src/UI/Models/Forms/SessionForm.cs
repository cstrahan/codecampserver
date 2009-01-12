using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.UI.Models.Forms
{
	public class SessionForm : EditForm<SessionForm>
	{
		public override Guid Id { get; set; }
		public virtual string Key { get; set; }
		public virtual TrackForm Track { get; set; }
		public virtual TimeSlotForm TimeSlot { get; set; }
		public virtual SpeakerForm Speaker { get; set; }
		public virtual Conference Conference { get; set; }
		public virtual string RoomNumber { get; set; }
		public virtual string Title { get; set; }
		public virtual string Abstract { get; set; }
		public virtual SessionLevel Level { get; set; }
		public virtual string MaterialsUrl { get; set; }
	}
}