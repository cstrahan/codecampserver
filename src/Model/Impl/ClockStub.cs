using System;

namespace CodeCampServer.Model.Impl
{
	public class ClockStub : IClock
	{
		private DateTime _time;

		public ClockStub(DateTime time)
		{
			_time = time;
		}

		public ClockStub()
		{
			
		}

		public DateTime GetCurrentTime()
		{
			return _time;
		}
	}
}