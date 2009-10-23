using System;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.Login
{
	public class LoginUserCommandMessage : ICommandMessage
	{
		public string Username { get; set; }

		public string Password { get; set; }
	}
}