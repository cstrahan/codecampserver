using System.Web.Mvc;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.UI.Binders.Entities
{
	public class EntityModelBinder : IFilteredModelBinder
	{
		private readonly IEntityModelBinderFactory _factory;

		public EntityModelBinder(IEntityModelBinderFactory factory)
		{
			_factory = factory;
		}

		public BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			IEntityModelBinder binder = _factory.GetEntityModelBinder(bindingContext.ModelType);

			return binder.BindModel(controllerContext, bindingContext);
		}

		public bool ShouldBind(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return typeof (PersistentObject).IsAssignableFrom(bindingContext.ModelType);
		}
	}
}