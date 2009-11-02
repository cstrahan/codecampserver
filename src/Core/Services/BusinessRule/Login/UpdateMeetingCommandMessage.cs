using System;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.Login
{
	public class LoginUserCommandMessage 
	{
		public string Username { get; set; }

		public string Password { get; set; }
	}
}