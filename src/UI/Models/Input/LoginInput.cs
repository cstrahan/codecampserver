using Castle.Components.Validator;

namespace CodeCampServer.UI.Models.Input
{
	public class LoginInput
	{
		[ValidateNonEmpty]
		public string Username { get; set; }

		[ValidateNonEmpty]		
		public string Password { get; set; }
	}
}