using System.Security;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;

namespace CodeCampServer.Website.Controllers
{
	public class LoginController : Controller
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
		    var numberOfUsers = getNumberOfUsers();
		    ViewData["ShowFirstTimeRegisterLink"] = (numberOfUsers == 0);

			RenderView("loginform", ViewData);
		}

	    private int getNumberOfUsers()
	    {
	        return _personRepository.GetNumberOfUsers();
	    }

	    public void Process(string email, string password, string redirectUrl)
		{
			if (_loginService.VerifyAccount(email, password))
			{
			    var person = _personRepository.FindByEmail(email);
				_authenticationService.SignIn(person);
				Redirect(redirectUrl ?? "~/default.aspx");
			}
			else
			{
			    TempData["Error"] = "Invalid login.";
				RenderView("loginform");
			}
		}

		public virtual void Redirect(string url)
		{
			Response.Redirect(url);
		}

        public void CreateAdminAccount(string firstName, string lastName, string email, string password, string passwordConfirm)
        {
            if (getNumberOfUsers() > 0)
            {
                throw new SecurityException(
                    "This action is only valid when there are no registered users in the system.");
            }

            var task = new CreateAdminAccountTask(_personRepository, _loginService, firstName, 
                lastName, email, password, passwordConfirm);

            task.Execute();

            if (!task.Success)
            {
                TempData["error"] = task.ErrorMessage;
            }
        	  
            RedirectToAction("index");
        }
	}

    public interface ITask
    {
        void Execute();
        bool Success { get; }
        string ErrorMessage { get;}
    }
}