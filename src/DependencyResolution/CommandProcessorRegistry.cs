using CodeCampServer.Infrastructure.CommandProcessor;
using StructureMap.Configuration.DSL;
using Tarantino.RulesEngine;

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