

using CodeCampServer.Core;

namespace CodeCampServer.DependencyResolution
{
	public class FactoryConfiguration : IRequiresConfigurationOnStartup
	{
		public void Configure()
		{
			SystemClockFactory.Default = () => new SystemClock();
			LoggerFactory.Default = () => new Logger();
		}
	}
}