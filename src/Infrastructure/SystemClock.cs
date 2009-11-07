using System;
using CodeCampServer.Core.Services.Bases;

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