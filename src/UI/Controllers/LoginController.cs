using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Input;

namespace CodeCampServer.UI.Controllers
{
	public class LoginController : SmartController
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly IUserRepository _repository;
		private readonly IUserSession _userSession;

		public LoginController(IAuthenticationService authenticationService, IUserRepository repository,
		                       IUserSession userSession)
		{
			_authenticationService = authenticationService;
			_repository = repository;
			_userSession = userSession;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ViewResult Index(string username)
		{
			var model = new LoginInput{Username = username};
			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateModel(typeof (LoginInput))]
		public ViewResult Index(LoginInput input)
		{
			if (!ModelState.IsValid)
			{
				return View(input);
			}

			LoginAndRedirect(input);

			ModelState.AddModelError("Login", "Login incorrect");
			return View(input);
		}

		private void LoginAndRedirect(LoginInput input)
		{
			User user = _repository.GetByUserName(input.Username);
			if (user != null)
			{
				if (PasswordMatches(input, user))
				{
					_userSession.LogIn(user);
				}
			}
		}

		private bool PasswordMatches(LoginInput input, User user)
		{
			return _authenticationService.PasswordMatches(user, input.Password);
		}

		public ActionResult LogOut()
		{
			_userSession.LogOut();
			throw new InvalidOperationException("If the previous line doesn't redirect, we have a problem.");
		}
	}
}