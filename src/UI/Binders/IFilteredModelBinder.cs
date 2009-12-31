using System.Web.Mvc;

namespace CodeCampServer.UI.Binders
{
	public interface IFilteredModelBinder
	{
		bool ShouldBind(ControllerContext controllerContext, ModelBindingContext bindingContext);
		BindResult BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
	}
}