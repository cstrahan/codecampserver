using System.Web.Mvc;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class LoginController : ApplicationController
	{
		private readonly ILoginService _loginService;
		private readonly IAuthenticationService _authenticationService;

		public LoginController(ILoginService loginService, IAuthenticationService authenticationService, IAuthorizationService authService) 
            :base(authService)
		{
			_loginService = loginService;
			_authenticationService = authenticationService;
		}

		[ControllerAction]
		public void Index()
		{
			RenderView("loginform", this.SmartBag);
		}

		[ControllerAction]
		public void Process(string email, string password, string redirectUrl)
		{
			if (_loginService.VerifyAccount(email, password))
			{
				_authenticationService.SetActiveUserName(email);
				Redirect(redirectUrl ?? "~/default.aspx");
			}
			else
			{
				RenderView("loginfailed");
			}
		}

		public virtual void Redirect(string url)
		{
			Response.Redirect(url);
		}
	}
}