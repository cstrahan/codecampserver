using System;

namespace CodeCampServer.Core.Services.Bases
{
	public interface ISystemClock
	{
		DateTime Now();
	}
}