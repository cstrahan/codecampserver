using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;

namespace CodeCampServer.Core.Services.Updaters
{
    public interface IConferneceUpdater : IModelUpdater<Conference, IAttendeeMessage>
    {
    }
}