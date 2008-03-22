using System;

namespace CodeCampServer.Model.Impl
{
	public class SystemClock : IClock
	{
		public DateTime GetCurrentTime()
		{
			return DateTime.Now;
		}
	}
}