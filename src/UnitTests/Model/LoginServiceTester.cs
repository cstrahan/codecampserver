using NUnit.Framework;
using Rhino.Mocks;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
namespace CodeCampServer.UnitTests.Model
{
    [TestFixture]
    public class LoginServiceTester
    {        
        [Test]
        public void ShouldGetUserAccountByEmail()
        {
            MockRepository mocks = new MockRepository();
            IPersonRepository repository = mocks.CreateMock<IPersonRepository>();

            string email = "brownie@brownie.com.au";
            string password = "password";

            Person person = new Person("test", "person", "");
            person.Contact.Email = email;
            person.Password = "bmPVWya622eCBIZNaniLf4H27pI=";
            person.PasswordSalt = "BrEz0Iqihh8ybLLz3THarw94LKsiO0oqE7dP3aqm796gnZdmvqZi/IZHY5LeWjL5CkQJIWr/QKlUanckJIFm4A==";
            
            SetupResult.For(repository.FindByEmail(email)).Return(person);
            mocks.ReplayAll();

            ILoginService service = new LoginService(repository);
            bool authenticationSuccessful = service.VerifyAccount(email, password);
            Assert.IsTrue(authenticationSuccessful);

            bool authenticationFailure = service.VerifyAccount(email, password + "extra");
            Assert.IsFalse(authenticationFailure);
        }

        [Test]
        public void VerifyAuthenticationFailureOnAccountNotFound()
        {
            MockRepository mocks = new MockRepository();
            IPersonRepository repository = mocks.CreateMock<IPersonRepository>();

            string email = "brownie@brownie.com.au";
            string password = "password";

            SetupResult.For(repository.FindByEmail(email)).Return(null);
            mocks.ReplayAll();

            ILoginService service = new LoginService(repository);
            bool authenticationFailure = service.VerifyAccount(email, password);

            Assert.IsFalse(authenticationFailure);

        }      
    }
}
