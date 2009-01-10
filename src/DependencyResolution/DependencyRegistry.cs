using Castle.Components.Validator;
using CodeCampServer.Infrastructure.AutoMap;
using CodeCampServer.UI.Helpers.ViewPage;
using CodeCampServer.UI.Helpers.ViewPage.InputBuilders;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace CodeCampServer.DependencyResolution
{
	public class DependencyRegistry : Registry
	{
		protected override void configure()
		{
			string name = GetType().Assembly.GetName().Name;
			name = name.Substring(0, name.LastIndexOf("."));

			Scan(x =>
			     	{
			     		x.Assembly(name + ".Core");
			     		x.Assembly(name + ".Infrastructure");
			     		x.Assembly(name + ".UI");
			     		x.With<DefaultConventionScanner>();
			     	});

			ForRequestedType<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();

			ForRequestedType<IInputBuilderFactory>()
				.TheDefault.Is.OfConcreteType<InputBuilderFactory>()
				.TheArrayOf<IInputBuilder>()
				.Contains(x =>
				          	{
				          		x.OfConcreteType<HiddenInputBuilder>();
				          		x.OfConcreteType<CheckboxInputBuilder>();
				          		x.OfConcreteType<YesNoRadioInputBuilder>();
				          		x.OfConcreteType<RadioInputBuilder>();
				          		x.OfConcreteType<DateInputBuilder>();
				          		x.OfConcreteType<EnumerationInputBuilder>();
				          		x.OfConcreteType<TextBoxInputBuilder>();
				          	});
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
			ForRequestedType<IMappingEngine>().TheDefault.Is.ConstructedBy(() => AutoMapper.Engine);
		}
	}
}