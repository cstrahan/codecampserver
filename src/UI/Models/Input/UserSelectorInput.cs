using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Binders;

namespace CodeCampServer.UI.Models.Input
{
	[TypeConverter(typeof (UserSelectorInputTypeConverter))]
	public class UserSelectorInput
	{
		[Required()]
		public virtual string Name { get; set; }

		[Hidden]
		public Guid Id { get; set; }

		[Required()]
		[RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",
			ErrorMessage = "Username is not valid.")]
		public virtual string Username { get; set; }
	}
}