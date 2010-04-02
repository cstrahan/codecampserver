using BatchJobs.Core;
using CodeCampServer.Core;


namespace CodeCampServer.DependencyResolution
{
	public class JobAgentProxy : IJobAgent
	{
		private readonly IBatchJobAgent _agent;

		public JobAgentProxy(IBatchJobAgent agent)
		{
			_agent = agent;
		}

		public void Execute()
		{
			_agent.Execute();
		}
	}
}