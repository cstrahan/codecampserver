using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteMeeting
{
	public class DeleteMeetingCommandHandler:Command<DeleteMeetingCommandMessage>
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

	interface ICommandHandler<TMessage>
	{
		void Execute(TMessage message);
	}

	interface ICommandHandler<TMessage, TReturn>
	{
		TReturn Execute(TMessage message);
	}
}