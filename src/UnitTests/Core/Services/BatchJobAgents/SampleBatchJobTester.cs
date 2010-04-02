using CodeCampServer.Core.Services.BatchJobAgents;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Services.BatchJobAgents
{
	[TestFixture]
	public class SampleBatchJobTester
	{
		[Test]
		public void Testing_execute_of_simple_batch_job()
		{
			var batchjob = new SampleBatchJob();
			batchjob.Execute();
		}
	}
}