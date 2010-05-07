using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule
{
	public class DeleteMeetingCommandMessage
	{
		public Meeting Meeting { get; set; }
	}
}