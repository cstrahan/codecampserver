using CodeCampServer.Model.Domain;
using System.Security.Cryptography;
using System;
using System.Text;

namespace CodeCampServer.Model.Impl
{
    public class LoginService : ILoginService
    {
	    private readonly IPersonRepository _personRepository;

        public LoginService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public bool VerifyAccount(string email, string password)
        {
            Person person = _personRepository.FindByEmail(email);
            if (person == null) return false;

            string passwordHash = CreatePasswordHash(password, person.PasswordSalt);
            return passwordHash == person.Password;
        }

        /// <summary>
        /// Create salt for encrypting user passwords.  
        /// Original Source: http://davidhayden.com/blog/dave/archive/2004/02/16/157.aspx
        /// </summary>
        /// <returns></returns>
        public string CreateSalt()
        {
            int size = 64;
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

	    /// <summary>
        /// Create a password hash based on a password and salt.  
        /// Adapted from: http://davidhayden.com/blog/dave/archive/2004/02/16/157.aspx
        /// </summary>
        /// <returns></returns>
        public string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);

            HashAlgorithm algorithm = SHA1.Create();
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd));

            return Convert.ToBase64String(hash);
        }
    }
}