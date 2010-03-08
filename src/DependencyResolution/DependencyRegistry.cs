using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.CommandProcessor;
using CodeCampServer.UI.Helpers.Filters;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace CodeCampServer.DependencyResolution
{
    public class DependencyRegistry : Registry
    {
        public DependencyRegistry()
        {
            string assemblyPrefix = GetThisAssembliesPrefix();

            Scan(x =>
                     {
                         IEnumerable<AssemblyName> enumerable = GetType().Assembly.GetReferencedAssemblies()
                             .Where(name => name.Name.StartsWith(assemblyPrefix));
                         enumerable.ForEach(name => x.Assembly(name.Name));
                         x.Assembly("MvcContrib.CommandProcessor");
                         x.With<DefaultConventionScanner>();
                         x.LookForRegistries();
                         x.AddAllTypesOf<IRequiresConfigurationOnStartup>();
                         x.AddAllTypesOf<IConventionActionFilter>();
                     });

            For<IRulesEngine>().Use<RulesEngine>();
            For<ISystemClock>().Use<SystemClock>();
        }

        private string GetThisAssembliesPrefix()
        {
            string name = GetType().Assembly.GetName().Name;
            name = name.Substring(0, name.LastIndexOf("."));
            return name;
        }
    }
}