using CodeCampServer.Core.Services.BusinessRule.CreateMeeting;
using CodeCampServer.UI.Models.Input;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.BusinessRules
{
	public class UpdateMeetingMessageConfiguration : MessageDefinition<MeetingInput>
	{
		public UpdateMeetingMessageConfiguration()
		{
			Execute<UpdateMeetingCommandMessage>();
		}
	}
}