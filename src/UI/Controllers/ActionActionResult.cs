using System;
using System.Web.Mvc;

namespace CodeCampServer.UI.Controllers
{
	public class ActionActionResult : ActionResult
	{
		private readonly Action _action;

		public ActionActionResult(Action action)
		{
			_action = action;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			_action.Invoke();
		}
	}
}