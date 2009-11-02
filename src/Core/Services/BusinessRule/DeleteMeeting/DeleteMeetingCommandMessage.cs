using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteMeeting
{
	public class DeleteMeetingCommandMessage 
	{
		public Meeting Meeting { get; set; }
	}
}