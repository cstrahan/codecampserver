using CodeCampServer.Model.Domain;

namespace CodeCampServer.Website.Controllers
{
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
            if (!Validate()) return;

            var person = new Person(_firstName, _lastName, _email);
            person.PasswordSalt = _loginService.CreateSalt();
            person.Password = _loginService.CreatePasswordHash(_password, person.PasswordSalt);
            person.IsAdministrator = true;

            _repository.Save(person);
            Success = true;
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
}