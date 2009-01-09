using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.UI.Models.Forms
{
	public class AttendeeForm : ValueObject<AttendeeForm>, IAttendeeMessage
	{
		public Guid ConferenceID { get; set; }
		public Guid? AttendeeID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public AttendanceStatus Status { get; set; }
		public string Webpage { get; set; }
	}
}