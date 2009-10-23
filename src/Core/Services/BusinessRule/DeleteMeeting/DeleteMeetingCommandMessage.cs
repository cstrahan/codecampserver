using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteMeeting
{
	public class DeleteMeetingCommandMessage : ICommandMessage
	{
		public Meeting Meeting { get; set; }
	}
}