using System;
using CodeCampServer.UI.Helpers.Mappers;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class SpeakerForm : ISpeakerMessage
	{
		public Guid Id { get; set; }
		[BetterValidateNonEmpty("Url Key")]
		public string Key { get; set; }
		[BetterValidateNonEmpty("First Name")]
		public string FirstName { get; set; }
		[BetterValidateNonEmpty("Last Name")]
		public string LastName { get; set; }
		public string Company { get; set; }
		[BetterValidateEmail("Email")]
		public string EmailAddress { get; set; }
		
		public string JobTitle { get; set; }
		
		public string Bio { get; set; }
		
		public string WebsiteUrl { get; set; }
	}
}