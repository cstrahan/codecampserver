using CodeCampServer.Core.Services;
using LoginPortableArea.Areas.Login.Messages;
using MvcContrib.PortableAreas;

namespace CodeCampServer.UI
{
	public class LoginHandler : MessageHandler<LoginInputMessage>
	{
		private readonly IRulesEngine _rulesEngine;

		public LoginHandler(IRulesEngine rulesEngine)
		{
			_rulesEngine = rulesEngine;
		}

		public override void Handle(LoginInputMessage message)
		{
			var uimessage = new LoginInputProxy {Password = message.Input.Password, Username = message.Input.Username};

			ICanSucceed result = _rulesEngine.Process(uimessage);

			if (result.Successful)
			{
				message.Result.Success = true;
				message.Result.Username = message.Input.Username;
			}

			foreach (var errorMessage in result.Errors)
			{
				message.Result.Message += errorMessage.Message;
			}
		}
	}

	public class LoginInputProxy
	{
		public string Password { get; set; }
		public string Username { get; set; }
	}
}