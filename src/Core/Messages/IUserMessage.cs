using System;

namespace CodeCampServer.Core.Messages
{
	public interface IUserMessage : IFormMessage
	{
		Guid Id { get; set; }
		string Name { get; set; }
		string EmailAddress { get; set; }
		string Password { get; set; }
		string Username { get; set; }
	}
}