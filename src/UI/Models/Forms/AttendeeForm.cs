using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Models.Forms
{
	public class AttendeeForm : ValueObject<AttendeeForm>
	{
		public virtual Guid ConferenceID { get; set; }
		public virtual Guid? AttendeeID { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual AttendanceStatus Status { get; set; }
		public virtual string Webpage { get; set; }
	}
}