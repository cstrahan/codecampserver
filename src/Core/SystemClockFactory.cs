using System;

namespace CodeCampServer.Core
{
	public class SystemClockFactory : AbstractFactoryBase<ISystemClock>
	{
		public static Func<ISystemClock> Default = DefaultUnconfiguredState;
	}
}