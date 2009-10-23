using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.CreateMeeting
{
	public class UpdateMeetingCommandMessage : ICommandMessage
	{
		public Meeting Meeting { get; set; }
	}
}