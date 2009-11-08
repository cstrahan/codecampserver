using CodeCampServer.Core;

namespace CodeCampServer.Infrastructure
{
	public class FactoryConfiguration : IRequiresConfigurationOnStartup
	{
		public void Configure()
		{
			SystemClockFactory.Default = () => new SystemClock();
		}
	}
}