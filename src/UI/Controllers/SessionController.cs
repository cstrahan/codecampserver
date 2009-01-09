using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;

namespace CodeCampServer.UI.Controllers
{
	public class SessionController : SaveController<Session, ISessionMessage>
	{
		private readonly ISessionUpdater _updater;

		public SessionController(ISessionUpdater updater)
		{
			_updater = updater;
		}

		protected override IModelUpdater<Session, ISessionMessage> GetUpdater()
		{
			return _updater;
		}
	}
}