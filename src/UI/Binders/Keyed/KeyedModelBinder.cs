using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.UI.Binders.Keyed
{
	public class KeyedModelBinder : IFilteredModelBinder
	{
		private readonly IKeyedModelBinderFactory _factory;

		public KeyedModelBinder(IKeyedModelBinderFactory factory)
		{
			_factory = factory;
		}

		public BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			IKeyedModelBinder binder = GetKeyedModelBinder(bindingContext.ModelType);
			return binder.BindModel(controllerContext, bindingContext);
		}

		public bool ShouldBind(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return typeof (KeyedObject).IsAssignableFrom(bindingContext.ModelType);
		}

		private IKeyedModelBinder GetKeyedModelBinder(Type entityType)
		{
			return _factory.GetBinder(entityType);
		}
	}
}