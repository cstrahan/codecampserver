using System.Web.Mvc;

namespace CodeCampServer.UI.Binders
{
	public class CompositionBinder : DefaultModelBinder
	{
		private readonly IFilteredModelBinder[] _binders;

		public CompositionBinder(IFilteredModelBinder[] filteredModelBinders)
		{
			_binders = filteredModelBinders;
		}

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			foreach (var filteredModelBinder in _binders)
			{
				if (filteredModelBinder.ShouldBind(controllerContext, bindingContext))
				{
					BindResult result = filteredModelBinder.BindModel(controllerContext, bindingContext);

					bindingContext.ModelState.SetModelValue(bindingContext.ModelName, result.ValueProviderResult);

					return result.Value;
				}
			}

			return base.BindModel(controllerContext, bindingContext);
		}
	}
}