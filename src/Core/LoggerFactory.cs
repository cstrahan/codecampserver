using System;

namespace CodeCampServer.Core
{
	public class LoggerFactory : AbstractFactoryBase<ILogger>
	{
		public static Func<ILogger> Default = () =>  new Logger();
	}
}