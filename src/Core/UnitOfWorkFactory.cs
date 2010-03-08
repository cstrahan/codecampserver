using System;

namespace CodeCampServer.Core
{
	public class UnitOfWorkFactory : AbstractFactoryBase<IUnitOfWork>
	{
		public static Func<IUnitOfWork> GetDefault = DefaultUnconfiguredState;
	}
}