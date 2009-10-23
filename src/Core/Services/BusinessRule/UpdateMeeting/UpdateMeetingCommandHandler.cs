using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services.BusinessRule.CreateMeeting;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateMeeting
{
	public class UpdateMeetingCommandHandler : Command<UpdateMeetingCommandMessage>
	{
		private readonly IMeetingRepository _meetingRepository;

		public UpdateMeetingCommandHandler(IMeetingRepository meetingRepository)
		{
			_meetingRepository = meetingRepository;
		}

		protected override ReturnValue Execute(UpdateMeetingCommandMessage commandMessage)
		{
			_meetingRepository.Save(commandMessage.Meeting);
			return new ReturnValue {Type = typeof (Meeting), Value = commandMessage.Meeting};
		}
	}
}