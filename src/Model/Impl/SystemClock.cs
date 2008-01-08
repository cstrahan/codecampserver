using System;
using StructureMap;

namespace CodeCampServer.Model.Impl
{
	[Pluggable(Keys.DEFAULT)]
	public class SystemClock : IClock
	{
		public DateTime GetCurrentTime()
		{
			return DateTime.Now;
		}
	}
}