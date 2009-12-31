using CodeCampServer.Core.Services.BusinessRule.UpdateMeeting;
using CodeCampServer.UI.Models.Input;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class UpdateMeetingMessageConfiguration : MessageDefinition<MeetingInput>
	{
		public UpdateMeetingMessageConfiguration()
		{
			Execute<UpdateMeetingCommandMessage>();
		}
	}
}