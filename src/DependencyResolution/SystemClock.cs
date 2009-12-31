using System;
using CodeCampServer.Core;

namespace CodeCampServer.DependencyResolution
{
	public class SystemClock : ISystemClock
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}
	}
}