using System.Collections.Generic;
using BatchJobs.Core;
using CodeCampServer.Core;
using ILogger=BatchJobs.Core.Logging.ILogger;

namespace CodeCampServer.DependencyResolution
{
	public class JobAgentFactory : IJobAgentFactory
	{

		public JobAgentFactory(ILogger batchRunnerlogger)
		{
			var localLogger = new Logger();
			localLogger.SetDebug(batchRunnerlogger.Debug);
			localLogger.SetError(batchRunnerlogger.Error);
			localLogger.SetWarn(batchRunnerlogger.Warn);
			localLogger.SetInfo(batchRunnerlogger.Info);
			localLogger.SetFatal(batchRunnerlogger.Fatal);
			LoggerFactory.Default = () => localLogger;
		}

		public IJobAgent Create(string name)
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			var agent = DependencyRegistrar.Resolve<IBatchJobAgent>(name);
			return new JobAgentProxy(agent);
		}

		public IEnumerable<string> GetInstanceNames()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			return DependencyRegistrar.GetInstanceNamesFor<IBatchJobAgent>();
		}
	}
}