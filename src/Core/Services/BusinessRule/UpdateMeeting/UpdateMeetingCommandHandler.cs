using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateMeeting
{
	public class UpdateMeetingCommandHandler : ICommand<UpdateMeetingCommandMessage, Meeting>
	{
		private readonly IMeetingRepository _meetingRepository;

		public UpdateMeetingCommandHandler(IMeetingRepository meetingRepository)
		{
			_meetingRepository = meetingRepository;
		}

		public Meeting Execute(UpdateMeetingCommandMessage commandMessage)
		{
			_meetingRepository.Save(commandMessage.Meeting);
			return commandMessage.Meeting;
		}
	}
}