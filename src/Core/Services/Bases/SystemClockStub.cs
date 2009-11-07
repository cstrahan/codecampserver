using System;

namespace CodeCampServer.Core.Services.Bases
{
	public class ClockStub : ISystemClock
	{
		private DateTime _now;
		public ClockStub(DateTime now)
		{
			_now = now;
		}

		public DateTime Now()
		{
			return _now;
		}
	}
}