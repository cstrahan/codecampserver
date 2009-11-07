using System;

namespace CodeCampServer.Core
{
	public class SystemClockFactory : StaticFactory<ISystemClock>
	{
		public static Func<ISystemClock> Default = DefaultUnconfiguredState;
	}
}