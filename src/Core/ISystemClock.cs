using System;

namespace CodeCampServer.Core
{
	public interface ISystemClock
	{
		DateTime Now();
	}
}