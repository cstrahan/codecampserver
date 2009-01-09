using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Messages;
using CodeCampServer.Core.Services.Updaters;

namespace CodeCampServer.UI.Controllers
{
	public class SessionController : SaveController<Session, ISessionMessage>
	{
		private readonly ISessionUpdater _sessionUpdater;

		public SessionController(ISessionUpdater sessionUpdater)
		{
			_sessionUpdater = sessionUpdater;
		}

		protected override IModelUpdater<Session, ISessionMessage> GetUpdater()
		{
			return _sessionUpdater;
		}
	}
}