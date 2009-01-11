using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.UI.Helpers.Mappers;

namespace CodeCampServer.UI.Models.Forms
{
	public class SessionForm : EditForm<SessionForm>, ISessionMessage
	{
		public override Guid Id { get; set; }
		public string Key { get; set; }
		public Track Track { get; set; }
		public TimeSlot TimeSlot { get; set; }
		public Speaker Speaker { get; set; }
		public Conference Conference { get; set; }
		public string RoomNumber { get; set; }
		public string Title { get; set; }
		public string Abstract { get; set; }
		public SessionLevel Level { get; set; }
		public string MaterialsUrl { get; set; }
	}
}