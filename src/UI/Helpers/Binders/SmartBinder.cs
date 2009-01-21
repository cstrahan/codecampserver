using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Helpers.Binders;

using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.DependencyResolution
{
	public class SmartBinder : DefaultModelBinder
	{
		public override ModelBinderResult BindModel(ModelBindingContext bindingContext)
		{
			return base.BindModel(bindingContext);
		}	
		protected override ModelBinderResult BindModelCore(ModelBindingContext bindingContext)
		{
			if (ShouldBuildInstanceFromContainer(bindingContext.ModelType))
			{
				return BindFromContainer(bindingContext);
			}

			if (ShouldBuildInstanceFromKeyedModelBinder(bindingContext.ModelType))
			{
				return BindUsingKeyedModelBinder(bindingContext);
			}

			if (ShouldBuildInstanceFromModelBinder(bindingContext.ModelType))
			{
				return BindUsingModelBinder(bindingContext);
			}

			if (ShouldBuildInstanceForEnumeration(bindingContext.ModelType))
			{
				return BindUsingEnumerationBinder(bindingContext);
			}

			return base.BindModelCore(bindingContext);
		}

		private static ModelBinderResult BindFromContainer(ModelBindingContext bindingContext)
		{
			object instance = DependencyRegistrar.Resolve(bindingContext.ModelType);
			return new ModelBinderResult(instance);
		}

		private static ModelBinderResult BindUsingEnumerationBinder(ModelBindingContext bindingContext)
		{
			var binder = new EnumerationModelBinder();

			return binder.BindModel(bindingContext);
		}

		private static ModelBinderResult BindUsingModelBinder(ModelBindingContext bindingContext)
		{
			Type repositoryType = typeof (IRepository<>).MakeGenericType(bindingContext.ModelType);
			Type modelBinderType = typeof (ModelBinder<,>).MakeGenericType(bindingContext.ModelType, repositoryType);

			var binder = (IModelBinder)DependencyRegistrar.Resolve(modelBinderType);

			return binder.BindModel(bindingContext);
		}

		private static ModelBinderResult BindUsingKeyedModelBinder(ModelBindingContext bindingContext)
		{
			Type repositoryType = typeof(IKeyedRepository<>).MakeGenericType(bindingContext.ModelType);
			Type modelBinderType = typeof (KeyedModelBinder<,>).MakeGenericType(bindingContext.ModelType, repositoryType);

			var binder = (IModelBinder)DependencyRegistrar.Resolve(modelBinderType);

			return binder.BindModel(bindingContext);
		}

		private static bool ShouldBuildInstanceFromKeyedModelBinder(Type modelType)
		{
			return typeof (KeyedObject).IsAssignableFrom(modelType);
		}

		private static bool ShouldBuildInstanceForEnumeration(Type modelType)
		{
			return typeof (Enumeration).IsAssignableFrom(modelType);
		}

		private static bool ShouldBuildInstanceFromModelBinder(Type modelType)
		{
			return typeof (PersistentObject).IsAssignableFrom(modelType);
		}

		private static bool ShouldBuildInstanceFromContainer(Type type)
		{
			return (type.IsInterface || (typeof (IController)).IsAssignableFrom(type));
		}
	}
}