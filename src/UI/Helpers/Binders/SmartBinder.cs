using System;
using System.Collections;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.Models.Forms;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.UI.Helpers.Binders
{
    public class UserFormTypeConverter:System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
        {
            if(sourceType==typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var returnValue =new UserForm();
            returnValue.Id = new Guid(value.ToString());
            return returnValue;
            //return base.ConvertFrom(context, culture, value);
        }
        
    }
	public class SmartBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (ShouldBuildInstanceFromContainer(bindingContext.ModelType))
			{
				return BindFromContainer(controllerContext, bindingContext);
			}

			if (ShouldBuildInstanceFromKeyedModelBinder(bindingContext.ModelType))
			{
				return BindUsingKeyedModelBinder(controllerContext, bindingContext);
			}

			if (ShouldBuildInstanceFromModelBinder(bindingContext.ModelType))
			{
				return BindUsingModelBinder(controllerContext, bindingContext);
			}

			if (ShouldBuildInstanceForEnumeration(bindingContext.ModelType))
			{
				return BindUsingEnumerationBinder(controllerContext, bindingContext);
			}

			return base.BindModel(controllerContext, bindingContext);
		}

		private static object BindFromContainer(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			object instance = DependencyRegistrar.Resolve(bindingContext.ModelType);
			return instance;
		}

		private static object BindUsingEnumerationBinder(ControllerContext controllerContext,
		                                                 ModelBindingContext bindingContext)
		{
			var binder = new EnumerationModelBinder();

			return binder.BindModel(controllerContext, bindingContext);
		}

		private static object BindUsingModelBinder(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			Type repositoryType = typeof (IRepository<>).MakeGenericType(bindingContext.ModelType);
			Type modelBinderType = typeof (ModelBinder<,>).MakeGenericType(bindingContext.ModelType, repositoryType);

			var binder = (IModelBinder) DependencyRegistrar.Resolve(modelBinderType);

			return binder.BindModel(controllerContext, bindingContext);
		}

		private static object BindUsingKeyedModelBinder(ControllerContext controllerContext,
		                                                ModelBindingContext bindingContext)
		{
			Type repositoryType = typeof (IKeyedRepository<>).MakeGenericType(bindingContext.ModelType);
			Type modelBinderType = typeof (KeyedModelBinder<,>).MakeGenericType(bindingContext.ModelType, repositoryType);

			var binder = (IModelBinder) DependencyRegistrar.Resolve(modelBinderType);

			return binder.BindModel(controllerContext, bindingContext);
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
			return !TypeIsACollection(type) && TypeIsAnInterfaceOrController(type);
		}

	    private static bool TypeIsACollection(Type type)
	    {
	        return (typeof (IEnumerable)).IsAssignableFrom(type);
	    }

	    private static bool TypeIsAnInterfaceOrController(Type type)
	    {
	        return type.IsInterface || (typeof (IController)).IsAssignableFrom(type);
	    }
	}
}