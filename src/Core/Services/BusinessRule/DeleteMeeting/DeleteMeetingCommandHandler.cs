using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteMeeting
{
	public class DeleteMeetingCommandHandler : ICommand<DeleteMeetingCommandMessage,Meeting>
	{
		private readonly IMeetingRepository _meetingRepository;

		public DeleteMeetingCommandHandler(IMeetingRepository meetingRepository)
		{
			_meetingRepository = meetingRepository;
		}

		public Meeting Execute(DeleteMeetingCommandMessage commandMessage)
		{
			_meetingRepository.Delete(commandMessage.Meeting);
			return commandMessage.Meeting;
		}
	}
}