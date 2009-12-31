using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers.ActionResults
{
	public class RedirectToReturnUrlResult : ActionResult
	{
		public override void ExecuteResult(ControllerContext context)
		{
			var manager = ReturnUrlManagerFactory.GetDefault();
			var result = new RedirectResult(manager.GetReturnUrl());
			result.ExecuteResult(context);
		}
	}
}