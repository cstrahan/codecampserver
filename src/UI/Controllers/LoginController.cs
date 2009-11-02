using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Models.Input;
using CommandProcessor;
using Tarantino.RulesEngine;

namespace CodeCampServer.UI.Controllers
{
	public class LoginController : SmartController
	{
		private readonly IUserSession _userSession;
		private readonly IRulesEngine _rulesEngine;

		public LoginController(IUserSession userSession, IRulesEngine rulesEngine)
		{
			_userSession = userSession;
			_rulesEngine = rulesEngine;
		}


		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult Index(string username)
		{
			var model = new LoginInput {Username = username};
			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		//[ValidateModel(typeof (LoginInput))]
		public ViewResult Index(LoginInput input)
		{
			if (!ModelState.IsValid)
			{
				return View(input);
			}

			ExecutionResult result = _rulesEngine.Process(input);

			if (result.ReturnItems.Get<User>() != null)
			{
				_userSession.LogIn(result.ReturnItems.Get<User>());
			}
			ModelState.AddModelError("Login", "Login incorrect");
			return View(input);
		}

		public ActionResult LogOut()
		{
			_userSession.LogOut();
			throw new InvalidOperationException("If the previous line doesn't redirect, we have a problem.");
		}
	}
}