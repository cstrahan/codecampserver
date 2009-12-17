using System;

namespace CodeCampServer.Core
{
	public class AbstractFactoryBase<T>
	{
		protected static T DefaultUnconfiguredState()
		{
			throw new Exception(typeof (T).Name + " not configured.");
		}
	}
}