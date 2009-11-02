using System;
using System.ComponentModel.DataAnnotations;
using CodeCampServer.UI.Helpers.Attributes;
using CodeCampServer.UI.Helpers.Validation.Attributes;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Models.Input
{
	//[SameAs("ConfirmPassword","Password")]
	public class UserInput:IMessage
	{
		[Required()]
		[RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",ErrorMessage = "Username is not valid.")]
		public virtual string Username { get; set; }

		[Required()]
		public virtual string Name { get; set; }

		[Required()]
		[DataType(DataType.EmailAddress)]
		public virtual string EmailAddress { get; set; }

		[Hidden]
		public Guid Id { get; set; }

		[Required()]
		public virtual string Password { get; set; }

		
		[Required()]        		
		public virtual string ConfirmPassword { get; set; }
	}
}