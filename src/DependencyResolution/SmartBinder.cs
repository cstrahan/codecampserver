using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.DataAccess.Impl;
using CodeCampServer.UI.Helpers.Binders;
using StructureMap;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.DependencyResolution
{
	public class SmartBinder : DefaultModelBinder
	{
		protected override ModelBinderResult BindModelCore(ModelBindingContext bindingContext)
		{
			if (ShouldBuildInstanceFromContainer(bindingContext.ModelType))
			{
				return BindFromContainer(bindingContext);
			}

			if (ShouldBuildInstanceFromKeyedModelBinder(bindingContext))
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
			Type repositoryType = typeof (RepositoryBase<>).MakeGenericType(bindingContext.ModelType);
			Type modelBinderType = typeof (ModelBinder<,>).MakeGenericType(bindingContext.ModelType, repositoryType);

			var binder = (IModelBinder) ObjectFactory.GetInstance(modelBinderType);

			return binder.BindModel(bindingContext);
		}

		private static ModelBinderResult BindUsingKeyedModelBinder(ModelBindingContext bindingContext)
		{
			Type repositoryType = typeof (KeyedRepositoryBase<>).MakeGenericType(bindingContext.ModelType);
			Type modelBinderType = typeof (KeyedModelBinder<,>).MakeGenericType(bindingContext.ModelType, repositoryType);

			var binder = (IModelBinder) ObjectFactory.GetInstance(modelBinderType);

			return binder.BindModel(bindingContext);
		}

		private static bool ShouldBuildInstanceFromKeyedModelBinder(ModelBindingContext bindingContext)
		{
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

			return typeof (KeyedObject).IsAssignableFrom(bindingContext.ModelType);
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