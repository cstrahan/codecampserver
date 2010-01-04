using System.Web.Mvc;

namespace CodeCampServer.UI.InputBuilders.SelectListProvision
{
	public interface ISelectListProvider
	{
		SelectList Provide(object selected);
	}
}