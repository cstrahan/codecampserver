using System;

namespace CodeCampServer.Core.Messages
{
	public interface ITrackMessage
	{
		Guid Id { get; set; }
		string Name { get; set; }
		Guid ConferenceId { get; set; }
	}
}