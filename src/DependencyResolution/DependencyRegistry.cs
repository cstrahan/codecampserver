using CodeCampServer.Core;
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
				x.Assembly(assemblyPrefix + ".Core");
				x.Assembly(assemblyPrefix + ".Infrastructure");
				x.Assembly(assemblyPrefix + ".UI");
				x.Assembly("CommandProcessor");
				x.With<DefaultConventionScanner>();
				x.LookForRegistries();
				x.AddAllTypesOf<IRequiresConfigurationOnStartup>();
			});
		}

		private string GetThisAssembliesPrefix()
		{
			string name = GetType().Assembly.GetName().Name;
			name = name.Substring(0, name.LastIndexOf("."));
			return name;
		}
	}
}