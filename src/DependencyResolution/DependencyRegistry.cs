using AutoMapper;
using Castle.Components.Validator;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistry : Registry
	{
		protected override void configure()
		{
			string assemblyPrefix = GetThisAssembliesPrefix();

			Scan(x =>
			     	{
			     		x.Assembly(assemblyPrefix + ".Core");
			     		x.Assembly(assemblyPrefix + ".Infrastructure");
			     		x.Assembly(assemblyPrefix + ".UI");
			     		x.With<DefaultConventionScanner>();
							x.LookForRegistries();
			     	});


			ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
		}

		private string GetThisAssembliesPrefix()
		{
			string name = GetType().Assembly.GetName().Name;
			name = name.Substring(0, name.LastIndexOf("."));
			return name;
		}
	}


	public class CastleValidatorRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<IValidatorRunner>().TheDefault.Is.ConstructedBy(
				() => new ValidatorRunner(new CachedValidationRegistry()));
		}
	}

	public class AutoMapperRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<IMappingEngine>().TheDefault.Is.ConstructedBy(() => Mapper.Engine);
		}
	}
}