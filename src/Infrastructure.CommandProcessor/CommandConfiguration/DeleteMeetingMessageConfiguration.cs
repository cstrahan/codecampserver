using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.UI.Messages;
using MvcContrib.CommandProcessor.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class DeleteMeetingMessageConfiguration : MessageDefinition<DeleteMeetingMessage>
	{
		public DeleteMeetingMessageConfiguration()
		{
			Execute<DeleteMeetingCommandMessage>();
		}
	}
}