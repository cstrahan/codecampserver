using System;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUser
{
	public class UpdateUserCommandMessage : ICommandMessage
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public string Name { get; set; }

		public string EmailAddress { get; set; }

		public string ConfirmPassword { get; set; }

		public Guid Id { get; set; }
	}
}