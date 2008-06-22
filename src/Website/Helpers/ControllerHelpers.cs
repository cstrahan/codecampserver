using System.Web.Mvc;

namespace CodeCampServer.Website.Helpers
{
	public static class ControllerHelpers
	{
		/// <summary>
		/// Stores a temporary message (aka a "flash") for one-time display on a view.
		/// Call DisplayFlashMessage() from your view to display messages;
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="message"></param>
		public static void FlashMessage(this Controller controller, string message)
		{
			controller.TempData["message"] = message;
		}
	}
}