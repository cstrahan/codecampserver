using System;
using StructureMap;

namespace CodeCampServer.Model
{
	[PluginFamily(Keys.DEFAULT)]
	public interface IClock
	{
		DateTime GetCurrentTime();
	}
}