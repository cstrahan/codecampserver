using CodeCampServer.Model;
using CodeCampServer.Website.Helpers;

namespace CodeCampServer.Website.Controllers
{
	public class FlashMessageComponent : ComponentControllerBase
	{
		private readonly IUserSession _session;

		public FlashMessageComponent() : this(IoC.Resolve<IUserSession>())
		{
		}

		public FlashMessageComponent(IUserSession session)
		{
			_session = session;
		}

		public void GetMessages()
		{
			FlashMessage[] flashMessages = _session.PopUserMessages();
			RenderView("list", flashMessages);
		}
	}
}