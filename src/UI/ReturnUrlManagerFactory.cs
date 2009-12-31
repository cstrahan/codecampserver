using System;
using CodeCampServer.Core;
using CodeCampServer.UI.Services;

namespace CodeCampServer.UI
{
	public class ReturnUrlManagerFactory : AbstractFactoryBase<IReturnUrlManager>
	{
		public static Func<IReturnUrlManager> GetDefault = DefaultUnconfiguredState;
	}
}