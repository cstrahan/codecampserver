using CodeCampServer.Model.Domain;

namespace CodeCampServer.Model.Impl
{
    public class LoginService : ILoginService
    {
        private readonly ICryptoUtil _cryptoUtil;
	    private readonly IPersonRepository _personRepository;

        public LoginService(IPersonRepository personRepository, ICryptoUtil cryptoUtil)
        {
            _personRepository = personRepository;
            _cryptoUtil = cryptoUtil;
        }

        public bool VerifyAccount(string email, string password)
        {
            var person = _personRepository.FindByEmail(email);
            if (person == null) return false;

            var passwordHash = _cryptoUtil.HashPassword(password, person.PasswordSalt);
            return passwordHash == person.Password;
        }       
    }
}