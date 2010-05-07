using CodeCampServer.Core.Services.BusinessRule.CreateHeartbeat;
using CodeCampServer.UI.Models.Input;
using MvcContrib.CommandProcessor.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class HeartbeatConfiguration : MessageDefinition<CreateHeartbeatInput>
	{
		public HeartbeatConfiguration()
		{
			Execute<CreateHeartbeatCommandMessage>(); 
		}
	}
}