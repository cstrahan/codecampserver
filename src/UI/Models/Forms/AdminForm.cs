using System;
using Castle.Components.Validator;
using CodeCampServer.Core.Messages;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Forms.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
	public class UserForm : EditForm<UserForm>, IUserMessage
	{
		[BetterValidateNonEmpty("Name")]
		public string Name { get; set; }

		[BetterValidateNonEmpty("Email")]
		[BetterValidateEmail("Email")]
		public string EmailAddress { get; set; }

		[Hidden]
		public override Guid Id { get; set; }

		[BetterValidateNonEmpty("Password")]
		public string Password { get; set; }

		[BetterValidateNonEmpty("Username")]
		[ValidateRegExp(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
			"Username is not valid.")]
		public string Username { get; set; }

		[ValidateSameAs("Password")]
		[BetterValidateNonEmpty("Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}