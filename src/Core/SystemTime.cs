using System;

namespace CodeCampServer.Core
{
	public static class SystemTime
	{
		public static Func<DateTime> Now = () => DateTime.Now;
	}
}