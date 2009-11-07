using System;
using CodeCampServer.Core;

namespace CodeCampServer.Infrastructure
{
	public class SystemClock : ISystemClock
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}
	}
}