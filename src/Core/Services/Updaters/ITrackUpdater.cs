using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.Core.Services.Updaters
{
	public interface ITrackUpdater : IModelUpdater<Track, ITrackMessage>
	{
	}
}