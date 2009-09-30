using System;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	public class UserInput
	{
		[Required("Username")]
		[ValidateRegExp(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
			"Username is not valid.")]
		public virtual string Username { get; set; }

		[Required("Name")]
		public virtual string Name { get; set; }

		[Required("Email")]
		[RequiredEmail("Email")]
		public virtual string EmailAddress { get; set; }

		[Hidden]
		public Guid Id { get; set; }

		[Required("Password")]
		public virtual string Password { get; set; }

		[ValidateSameAs("Password")]
		[Required("Confirm Password")]
		public virtual string ConfirmPassword { get; set; }
	}
}