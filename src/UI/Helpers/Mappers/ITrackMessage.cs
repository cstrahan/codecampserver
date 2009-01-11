using System;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface ITrackMessage
	{
		Guid Id { get; set; }
		string Name { get; set; }
		Guid ConferenceId { get; set; }
	}
}