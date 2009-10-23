using Castle.Components.Validator;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Models.Input
{
	public class LoginInput : IMessage
	{
		[ValidateNonEmpty]
		public string Username { get; set; }

		[ValidateNonEmpty]		
		public string Password { get; set; }
	}
}