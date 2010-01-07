using System;
using CodeCampServer.UI.Binders;
using CodeCampServer.UI.Binders.Entities;
using CodeCampServer.UI.Binders.Keyed;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.DependencyResolution
{
	public class UiRegistry : Registry
	{
        [Obsolete("This should be turned in a filtered model binder factory that provides the concrete instances.  Too much behavior in the IoC configuration")]
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