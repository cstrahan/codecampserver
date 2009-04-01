using System;
using System.ComponentModel;
using Castle.Components.Validator;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Binders;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    [TypeConverter(typeof(UserFormTypeConverter))]
	public class UserForm : EditForm<UserForm>
	{
		[BetterValidateNonEmpty("Name")]
		public virtual string Name { get; set; }

		[BetterValidateNonEmpty("Email")]
		[BetterValidateEmail("Email")]
		public virtual string EmailAddress { get; set; }

		[Hidden]
		public override Guid Id { get; set; }

		[BetterValidateNonEmpty("Password")]
		public virtual string Password { get; set; }

		[BetterValidateNonEmpty("Username")]
		[ValidateRegExp(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
			"Username is not valid.")]
		public virtual string Username { get; set; }

		[ValidateSameAs("Password")]
		[BetterValidateNonEmpty("Confirm Password")]
		public virtual string ConfirmPassword { get; set; }
	}
}