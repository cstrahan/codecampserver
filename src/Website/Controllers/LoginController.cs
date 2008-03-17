using System.Security;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class LoginController : ApplicationController
	{
		private readonly ILoginService _loginService;
	    private readonly IPersonRepository _personRepository;
	    private readonly IAuthenticationService _authenticationService;

		public LoginController(ILoginService loginService, IPersonRepository personRepository, IAuthenticationService authenticationService, IAuthorizationService authService) 
            :base(authService)
		{
			_loginService = loginService;
		    _personRepository = personRepository;
		    _authenticationService = authenticationService;
           
		}

		public void Index()
		{
		    int numberOfUsers = getNumberOfUsers();
		    SmartBag.Add("ShowFirstTimeRegisterLink", numberOfUsers == 0);            

			RenderView("loginform", SmartBag);
		}

	    private int getNumberOfUsers()
	    {
	        return _personRepository.GetNumberOfUsers();
	    }

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

        public void CreateAdminAccount(string firstName, string lastName, string email, string password, string passwordConfirm)
        {
            if(getNumberOfUsers() > 0)
            {
                throw new SecurityException("This action is only valid when there are no registered users in the system.");
            }

            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["error"] = "Email and Password are required.";
                RedirectToAction("index");
            }

            if(password != passwordConfirm)
            {
                TempData["error"] = "Passwords must match";
                RedirectToAction("index");
            }
            
            Person admin = new Person(firstName, lastName, "");
            admin.Contact.Email = email;
            admin.PasswordSalt = _loginService.CreateSalt();
            admin.Password = _loginService.CreatePasswordHash(password, admin.PasswordSalt);
            admin.IsAdministrator = true;

            _personRepository.Save(admin);
            RedirectToAction("index");
        }
	}
}