using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Commands;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteMeeting
{
	public class DeleteMeetingCommandHandler : Command<DeleteMeetingCommandMessage>
	{
		private readonly IMeetingRepository _meetingRepository;

		public DeleteMeetingCommandHandler(IMeetingRepository meetingRepository)
		{
			_meetingRepository = meetingRepository;
		}

		protected override ReturnValue Execute(DeleteMeetingCommandMessage commandMessage)
		{
			_meetingRepository.Delete(commandMessage.Meeting);
			return new ReturnValue {Type = typeof (Meeting), Value = commandMessage.Meeting};
		}
	}
}