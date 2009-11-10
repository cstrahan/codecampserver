using System;
using System.Web.Mvc;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Services;

namespace CodeCampServer.UI.Controllers
{
	public class CommandResult<TInput,TResult> :CommandResult
	{
		private readonly TInput _message;
		private readonly Func<TResult, ActionResult> _success;
		private readonly Func<TInput, ActionResult> _failure;
		private TResult _result;

		public override ActionResult Success
		{
			get { return _success.Invoke(_result); }
		}

		public override ActionResult Failure { get { return _failure.Invoke(_message); } }

		public CommandResult(TInput message, Func<TResult,ActionResult> success, Func<TInput,ActionResult> failure)
		{
			_message = message;
			_success = success;
			_failure = failure;
		}

		public override void Execute(ControllerContext context)
		{
			var modelState = context.Controller.ViewData.ModelState;
			
			if (modelState.IsValid)
			{
				var rulesEngine = MvcContrib.Services.DependencyResolver.GetImplementationOf<IRulesEngine>();
				ICanSucceed result = rulesEngine.Process(_message);
				if (result.Successful)
				{
					_result = result.Result<TResult>();
					Success.ExecuteResult(context);
					return;					
				}

				foreach (ErrorMessage errorMessage in result.Errors)
				{
					modelState.AddModelError(UINameHelper.BuildNameFrom(errorMessage.InvalidProperty), errorMessage.Message);
				}
			}
			
			Failure.ExecuteResult(context);
		}
	}

	public abstract class CommandResult:ActionResult 
	{
		public abstract ActionResult Success { get; }
		public abstract ActionResult Failure { get; }
		public override void ExecuteResult(ControllerContext context)
		{
			Execute(context);
		}

		public abstract void Execute(ControllerContext context);
	}
}
