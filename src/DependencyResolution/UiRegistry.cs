using CodeCampServer.UI.Binders;
using CodeCampServer.UI.Binders.Entities;
using CodeCampServer.UI.Binders.Keyed;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.DependencyResolution
{
	public class UiRegistry : Registry
	{
		public UiRegistry()
		{
			ForConcreteType<CompositionBinder>()
				.Configure.TheArrayOf<IFilteredModelBinder>()
				.Contains(x =>
				          	{
				          		x.OfConcreteType<KeyedModelBinder>();
				          		x.OfConcreteType<EntityModelBinder>();
				          		x.OfConcreteType<EnumerationModelBinder>();
				          	});
		}
	}
}