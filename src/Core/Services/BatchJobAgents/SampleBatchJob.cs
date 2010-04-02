using System;

namespace CodeCampServer.Core.Services.BatchJobAgents
{
	public class SampleBatchJob : IBatchJobAgent
	{
		public void Execute()
		{
			LoggerFactory.Default().Info(this, "This is an empty batch process");
		}
	}
}