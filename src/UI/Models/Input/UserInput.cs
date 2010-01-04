using System;
using System.ComponentModel.DataAnnotations;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.InputBuilders.SelectListProvision;
using MvcContrib.UI.InputBuilder.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	//[SameAs("ConfirmPassword","Password")]
    [Label("User")]
	public class UserInput
	{
		[Required]
		[RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Username is not valid.")]
		public virtual string Username { get; set; }

		[Required]
		public virtual string Name { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public virtual string EmailAddress { get; set; }

		[Hidden]
		public Guid Id { get; set; }

		[Required]
		public virtual string Password { get; set; }

		[Required]
		public virtual string ConfirmPassword { get; set; }

//		[SelectListProvided(typeof(AllUsersSelectListProvider))]
//		public User DummyUser { get; set; }

//		[CheckboxList(typeof(AllUsersSelectListProvider))]
//		public User DummyUser { get; set; }
	}
}