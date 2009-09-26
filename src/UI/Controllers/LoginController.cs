using System;
using System.Web.Mvc;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using CodeCampServer.UI.Helpers.Filters;
using CodeCampServer.UI.Models.Forms;

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
		public ViewResult Index()
		{
			return View(new LoginForm());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateModel(typeof (LoginForm))]
		public ViewResult Index(LoginForm form)
		{
			if (!ModelState.IsValid)
			{
				return View(form);
			}

			LoginAndRedirect(form);

			ModelState.AddModelError("Login", "Login incorrect");
			return View(form);
		}

		private void LoginAndRedirect(LoginForm form)
		{
			User user = _repository.GetByUserName(form.Username);
			if (user != null)
			{
				if (PasswordMatches(form, user))
				{
					_userSession.LogIn(user);
				}
			}
		}

		private bool PasswordMatches(LoginForm form, User user)
		{
			return _authenticationService.PasswordMatches(user, form.Password);
		}

		public ActionResult LogOut()
		{
			_userSession.LogOut();
			throw new InvalidOperationException("If the previous line doesn't redirect, we have a problem.");
		}
	}
}