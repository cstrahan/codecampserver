using System.ComponentModel.DataAnnotations;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Models.Input
{
	public class LoginInput : IMessage
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}