using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateMeeting
{
	public class UpdateMeetingCommandMessage
	{
		public Meeting Meeting { get; set; }
	}
}