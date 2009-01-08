using System;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.UI.Models.Forms
{
	public class SpeakerForm : ISpeakerMessage
	{
		public Guid Id { get; set; }
		public string Key { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Company { get; set; }
		public string EmailAddress { get; set; }
		public string JobTitle { get; set; }
		public string Bio { get; set; }
		public string WebsiteUrl { get; set; }
	}
}