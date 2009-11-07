using System;

namespace CodeCampServer.Core
{
	public class Clock : ISystemClock
	{
		private readonly DateTime _now;

		public Clock(DateTime now)
		{
			_now = now;
		}

		public DateTime Now()
		{
			return _now;
		}
	}
}