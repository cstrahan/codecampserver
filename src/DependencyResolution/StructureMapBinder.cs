using System;
using System.Web.Mvc;
using StructureMap;

namespace CodeCampServer.DependencyResolution
{
	public class StructureMapBinder : DefaultModelBinder
	{
		protected override ModelBinderResult BindModelCore(ModelBindingContext bindingContext)
		{
			if (ShouldBuildInstanceFromStructureMap(bindingContext.ModelType))
			{
				object instance = ObjectFactory.GetInstance(bindingContext.ModelType);
				return new ModelBinderResult(instance);
			}
			return base.BindModelCore(bindingContext);
		}

		private static bool ShouldBuildInstanceFromStructureMap(Type type)
		{
			return (type.IsInterface || (typeof (IController)).IsAssignableFrom(type));
		}
	}
}