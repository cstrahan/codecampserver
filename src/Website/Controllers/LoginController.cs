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
		    int numberOfUsers = getNumberOfUsers();
		    ViewData.Add("ShowFirstTimeRegisterLink", numberOfUsers == 0);            

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
			    Person person = _personRepository.FindByEmail(email);
				_authenticationService.SignIn(person);
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
            if (getNumberOfUsers() > 0)
            {
                throw new SecurityException(
                    "This action is only valid when there are no registered users in the system.");
            }

            CreateAdminAccountTask task = new CreateAdminAccountTask(_personRepository, _loginService, 
                firstName, lastName, email, password, passwordConfirm);

            task.Execute();

            if (!task.Success)
            {
                TempData["error"] = task.ErrorMessage;
            }
        	  
            RedirectToAction("index");
        }
	}

    public class CreateAdminAccountTask : ITask
    {
        private readonly IPersonRepository _repository;
        private readonly ILoginService _loginService;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _email;
        private readonly string _password;
        private readonly string _passwordConfirm;

        public CreateAdminAccountTask(IPersonRepository repository, ILoginService loginService, 
            string name, string lastName, string email, string password, string passwordConfirm)
        {
            _repository = repository;
            _loginService = loginService;
            _firstName = name;
            _lastName = lastName;
            _email = email;
            _password = password;
            _passwordConfirm = passwordConfirm;
        }

        public void Execute()
        {
            if(Validate())
            {
                Person person = new Person(_firstName, _lastName, _email);
                person.PasswordSalt = _loginService.CreateSalt();
                person.Password = _loginService.CreatePasswordHash(_password, person.PasswordSalt);
                person.IsAdministrator = true;

                _repository.Save(person);
                Success = true;
            }                
        }

        private bool Validate()
        {
            if(string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password))
            {
                Success = false;
                ErrorMessage = "Email and Password are required";
                return false;
            }
            
            if(_password != _passwordConfirm)
            {
                Success = false;
                ErrorMessage = "Passwords must match.";
                return false;
            }

            return true;
        }

        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }
    }

    public interface ITask
    {
        void Execute();
        bool Success { get; }
        string ErrorMessage { get;}
    }
}