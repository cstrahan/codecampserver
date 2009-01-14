using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.ViewPage
{
	public interface IDisplayErrorMessages
	{
		string Display();
		ModelStateDictionary ModelState { set; }
		TempDataDictionary TempData { set; }
	}
}