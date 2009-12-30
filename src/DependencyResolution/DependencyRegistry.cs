using CodeCampServer.Core;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Services;
using CodeCampServer.Infrastructure.UI.Services;
using CodeCampServer.UI.Helpers.Filters;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using System.Linq;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistry : Registry
	{
		public DependencyRegistry()
		{
			string assemblyPrefix = GetThisAssembliesPrefix();

			Scan(x =>
			{
			    GetType().Assembly.GetReferencedAssemblies()
					.Where(name => name.Name.StartsWith(assemblyPrefix))
					.ForEach(name => x.Assembly(name.Name));

				x.Assembly("CommandProcessor");
				x.With<DefaultConventionScanner>();
				x.LookForRegistries();
				x.AddAllTypesOf<IRequiresConfigurationOnStartup>();
				x.AddAllTypesOf<IConventionActionFilter>();
			});

			ForRequestedType<IRulesEngine>().TheDefaultIsConcreteType<RulesEngine>();
		}

		private string GetThisAssembliesPrefix()
		{
			string name = GetType().Assembly.GetName().Name;
			name = name.Substring(0, name.LastIndexOf("."));
			return name;
		}
	}
}