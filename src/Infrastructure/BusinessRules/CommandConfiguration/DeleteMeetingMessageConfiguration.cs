using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.UI.Messages;
using Tarantino.RulesEngine.Configuration;

namespace CodeCampServer.Infrastructure.BusinessRules
{
	public class DeleteMeetingMessageConfiguration : MessageDefinition<DeleteMeetingMessage>
	{

		public DeleteMeetingMessageConfiguration()
		{
			Execute<DeleteMeetingCommandMessage>();
		}		
	}
}