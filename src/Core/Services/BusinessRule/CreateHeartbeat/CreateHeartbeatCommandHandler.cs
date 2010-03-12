using CodeCampServer.Core.Domain.Bases;
using MvcContrib.CommandProcessor;
using MvcContrib.CommandProcessor.Commands;

namespace CodeCampServer.Core.Services.BusinessRule.CreateHeartbeat
{
	public class CreateHeartbeatCommandHandler : Command<CreateHeartbeatCommandMessage>
	{
		private readonly IHeartbeatRepository _repository;
		private readonly ISystemClock _clock;

		public CreateHeartbeatCommandHandler(IHeartbeatRepository repository, ISystemClock clock)
		{
			_repository = repository;
			_clock = clock;
		}

		protected override ReturnValue Execute(CreateHeartbeatCommandMessage commandMessage)
		{
			var heartbeat = new Heartbeat {Message = commandMessage.Message, Date = _clock.Now()};
			_repository.Save(heartbeat);
			return new ReturnValue{Type = typeof(Heartbeat), Value = heartbeat};
		}
	}
}