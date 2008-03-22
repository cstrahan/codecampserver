using System;

namespace CodeCampServer.Model
{
	public interface IClock
	{
		DateTime GetCurrentTime();
	}
}