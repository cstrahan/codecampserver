using System.Web.Mvc;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class LoginController : Controller
	{
		private readonly ILoginService _loginService;
		private readonly IAuthenticationService _authenticationService;

		public LoginController(ILoginService loginService, IAuthenticationService authenticationService)
		{
			_loginService = loginService;
			_authenticationService = authenticationService;
		}

		[ControllerAction]
		public void Index()
		{
			RenderView("loginform");
		}

		[ControllerAction]
		public void Process(string email, string password, string redirectUrl)
		{
			if (_loginService.VerifyAccount(email, password))
			{
				_authenticationService.SetActiveUser(email);
				Redirect(redirectUrl);
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