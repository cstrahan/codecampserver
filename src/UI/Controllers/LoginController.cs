using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.ActionResults;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class LoginController : ConventionController
	{
		private readonly IUserSession _userSession;

		public LoginController(IUserSession userSession)
		{
			_userSession = userSession;
		}


		[HttpGet]
		public ViewResult Index(string username)
		{
			var model = new LoginInput {Username = username};
			return View(model);
		}

		[HttpPost]
		public CommandResult Index(LoginInput input)
		{
			return Command<LoginInput, User>(input,
			                                 r => Action(() => _userSession.LogIn(r)),
			                                 r => View(input));
		}


		public ActionResult LogOut()
		{
			_userSession.LogOut();
			throw new InvalidOperationException("If the previous line doesn't redirect, we have a problem.");
		}
	}
}