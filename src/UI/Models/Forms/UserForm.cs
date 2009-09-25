using System;
using Castle.Components.Validator;
using CodeCampServer.Core;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using CodeCampServer.UI.Models.Validation.Attributes;

namespace CodeCampServer.UI.Models.Forms
{
    //    [TypeConverter(typeof(UserFormTypeConverter))]
	public class UserForm : ValueObject<UserForm>
	{
		[BetterValidateNonEmpty("Username")]
		[ValidateRegExp(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
			"Username is not valid.")]
		public virtual string Username { get; set; }

		[BetterValidateNonEmpty("Name")]
		public virtual string Name { get; set; }

		[BetterValidateNonEmpty("Email")]
		[BetterValidateEmail("Email")]
		public virtual string EmailAddress { get; set; }

		[Hidden]
		public Guid Id { get; set; }

		[BetterValidateNonEmpty("Password")]
		public virtual string Password { get; set; }

		[ValidateSameAs("Password")]
		[BetterValidateNonEmpty("Confirm Password")]
		public virtual string ConfirmPassword { get; set; }
	}
}