using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule
{
	public class LoginProxyCommandMessage
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

namespace CodeCampServer.Core.Services.BusinessRule
{
	public class UpdateMeetingCommandMessage
	{
		public Meeting Meeting { get; set; }
	}
}