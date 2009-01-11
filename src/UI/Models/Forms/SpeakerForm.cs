using System;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class SpeakerForm
	{
		public virtual Guid Id { get; set; }

		[BetterValidateNonEmpty("Url Key")]
		public virtual string Key { get; set; }

		[BetterValidateNonEmpty("First Name")]
		public virtual string FirstName { get; set; }

		[BetterValidateNonEmpty("Last Name")]
		public virtual string LastName { get; set; }

		public virtual string Company { get; set; }

		[BetterValidateEmail("Email")]
		public virtual string EmailAddress { get; set; }

		public virtual string JobTitle { get; set; }

		public virtual string Bio { get; set; }

		public virtual string WebsiteUrl { get; set; }
	}
}