using System;

namespace CodeCampServer.Core.Messages
{
	public interface ISpeakerMessage
	{
		Guid Id { get; set; }
		string Key { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		string Company { get; set; }
		string EmailAddress { get; set; }
		string JobTitle { get; set; }
		string Bio { get; set; }
		string WebsiteUrl { get; set; }
	}
}