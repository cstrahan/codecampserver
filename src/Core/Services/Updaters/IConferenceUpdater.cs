using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.Core.Services.Updaters
{
	public interface IConferenceUpdater : IModelUpdater<Conference, IConferenceMessage>
	{
	}

	public interface IAttendeeUpdater : IModelUpdater<Conference, IAttendeeMessage>
	{
	}
}