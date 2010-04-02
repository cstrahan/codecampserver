using CodeCampServer.Core;
using CodeCampServer.Core.Services.BatchJobAgents;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.DependencyResolution
{
	public class WorkflowRegistry : Registry
	{
		public WorkflowRegistry()
		{
			ForRequestedType<IBatchJobAgent>().AddInstances(
				x => x.OfConcreteType<SampleBatchJob>().WithName("samplebatchjob"));

			ForRequestedType<IBatchJobAgent>().AddInstances(
				x => x.OfConcreteType<AnotherBatchJob>().WithName("anotherbatchjob"));

		}
	}
}