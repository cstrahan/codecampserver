using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Services.BusinessRule.CreateHeartbeat
{
	public class CreateHeartbeatCommandHandler : ICommand<CreateHeartbeatCommandMessage,Heartbeat>
	{
		private readonly IHeartbeatRepository _repository;
		private readonly ISystemClock _clock;

		public CreateHeartbeatCommandHandler(IHeartbeatRepository repository, ISystemClock clock)
		{
			_repository = repository;
			_clock = clock;
		}

		public Heartbeat Execute(CreateHeartbeatCommandMessage commandMessage)
		{
			var heartbeat = new Heartbeat {Message = commandMessage.Message, Date = _clock.Now()};
			_repository.Save(heartbeat);
			return  heartbeat;
		}
	}
}