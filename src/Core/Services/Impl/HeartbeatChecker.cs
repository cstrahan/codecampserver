using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Services.Impl
{
	public class HeartbeatChecker : IHeartbeatChecker
	{
		public const string Success = "Heartbeat is alive and ticking.";
		public const string DeadTimeout = "DEAD!  {0} minute threshold exceeded.  Now [{1}], Last Heartbeat [{2}]";
		public const string DeadNoHeartbeat = "DEAD!  Zero heartbeats recorded.";
		public const string DeadFutureHeartbeat = "DEAD!  Heartbeat is in the future.  Now [{0}], Last Heartbeat [{1}]";

		private readonly IHeartbeatRepository _repository;
		private readonly ISystemClock _clock;

		public HeartbeatChecker(IHeartbeatRepository repository, ISystemClock clock)
		{
			_repository = repository;
			_clock = clock;
		}

		public string CheckHeartbeat(int timeout)
		{
			var heartbeat = _repository.GetLatest();
			
			if (heartbeat == null)
				return DeadNoHeartbeat;

			var now = _clock.Now();
			if (heartbeat.Date > now)
				return string.Format(DeadFutureHeartbeat, now, heartbeat.Date);

			if (heartbeat.Date.AddMinutes(timeout) < now)
				return string.Format(DeadTimeout, timeout, now, heartbeat.Date);

			return Success;
		}
	}
}