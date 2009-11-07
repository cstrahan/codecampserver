using System;

namespace CodeCampServer.Core
{
	public class StaticFactory<T>
	{
		protected static T DefaultUnconfiguredState()
		{
			throw new Exception(typeof (T).Name + " not configured.");
		}
	}
}