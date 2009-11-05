using System;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUser
{
	public class UpdateUserCommandMessage
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public string Name { get; set; }

		public string EmailAddress { get; set; }

		public string ConfirmPassword { get; set; }

		public Guid Id { get; set; }
	}
}