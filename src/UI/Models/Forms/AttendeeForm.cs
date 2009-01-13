using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class AttendeeForm : ValueObject<AttendeeForm>
	{
		public virtual Guid ConferenceID { get; set; }
		public virtual Guid? AttendeeID { get; set; }
		
		[BetterValidateNonEmpty("First Name")]
		public virtual string FirstName { get; set; }
		
		[BetterValidateNonEmpty("First Name")]
		public virtual string LastName { get; set; }
		
		[BetterValidateNonEmpty("Email")]
		[BetterValidateEmail("Email")]
		public virtual string EmailAddress { get; set; }
		public virtual AttendanceStatus Status { get; set; }
		
		public virtual string Webpage { get; set; }
	}
}