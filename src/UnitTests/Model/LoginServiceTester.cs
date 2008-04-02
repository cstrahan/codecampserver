using CodeCampServer.Model.Impl;
using NUnit.Framework;
using Rhino.Mocks;
using CodeCampServer.Model.Domain;

namespace CodeCampServer.UnitTests.Model
{
    [TestFixture]
    public class LoginServiceTester
    {        
        [Test]
        public void ShouldGetUserAccountByEmail()
        {
            var mocks = new MockRepository();
            var repository = mocks.CreateMock<IPersonRepository>();

            var email = "brownie@brownie.com.au";
            var password = "password";

            var person = new Person("test", "person", "");
            person.Contact.Email = email;
            person.Password = "bmPVWya622eCBIZNaniLf4H27pI=";
            person.PasswordSalt = "BrEz0Iqihh8ybLLz3THarw94LKsiO0oqE7dP3aqm796gnZdmvqZi/IZHY5LeWjL5CkQJIWr/QKlUanckJIFm4A==";
            
            SetupResult.For(repository.FindByEmail(email)).Return(person);
            mocks.ReplayAll();

            ILoginService service = new LoginService(repository, new CryptoUtil());
            var authenticationSuccessful = service.VerifyAccount(email, password);
            Assert.IsTrue(authenticationSuccessful);

            var authenticationFailure = service.VerifyAccount(email, password + "extra");
            Assert.IsFalse(authenticationFailure);
        }

        [Test]
        public void VerifyAuthenticationFailureOnAccountNotFound()
        {
            var mocks = new MockRepository();
            var repository = mocks.CreateMock<IPersonRepository>();

            var email = "brownie@brownie.com.au";
            var password = "password";

            SetupResult.For(repository.FindByEmail(email)).Return(null);
            mocks.ReplayAll();

            ILoginService service = new LoginService(repository, new CryptoUtil());
            var authenticationFailure = service.VerifyAccount(email, password);

            Assert.IsFalse(authenticationFailure);

        }      
    }
}
