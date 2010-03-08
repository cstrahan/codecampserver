using CodeCampServer.Infrastructure.CommandProcessor;
using MvcContrib.CommandProcessor.Interfaces;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.DependencyResolution
{
	public class CommandProcessorRegistry : Registry
	{
		protected override void configure()
		{
			For<IUnitOfWork>().Use<CommandProcessorUnitOfWorkProxy>();
		}
	}
}