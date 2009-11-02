using System.ComponentModel.DataAnnotations;

namespace CodeCampServer.UI.Models.Input
{
	public class LoginInput
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}