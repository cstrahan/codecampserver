using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Mappers;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public interface IConferenceUpdater : IModelUpdater<Conference, IConferenceMessage>
	{
	}

	public interface IAttendeeUpdater : IModelUpdater<Conference, IAttendeeMessage>
	{
	}
}