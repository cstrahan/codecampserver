using NUnit.Framework;
using Rhino.Mocks;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using Rhino.Mocks.Constraints;
namespace CodeCampServer.UnitTests.Model
{
    [TestFixture]
    public class LoginServiceTester
    {
        [Test]
        public void ShouldGetUserAccountByEmail()
        {
            MockRepository mocks = new MockRepository();
            IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();

            string email = "brownie@brownie.com.au";
            string password = "password";

            Attendee attendee = new Attendee(string.Empty, string.Empty);
            attendee.Contact.Email = email;
            attendee.Password = "bmPVWya622eCBIZNaniLf4H27pI=";
            attendee.PasswordSalt =
                "BrEz0Iqihh8ybLLz3THarw94LKsiO0oqE7dP3aqm796gnZdmvqZi/IZHY5LeWjL5CkQJIWr/QKlUanckJIFm4A==";
            SetupResult.For(repository.GetAttendeeByEmail(email)).Return(attendee);
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
            IAttendeeRepository repository = mocks.CreateMock<IAttendeeRepository>();

            string email = "brownie@brownie.com.au";
            string password = "password";

            SetupResult.For(repository.GetAttendeeByEmail(email)).Return(null);
            mocks.ReplayAll();

            ILoginService service = new LoginService(repository);
            bool authenticationFailure = service.VerifyAccount(email, password);

            Assert.IsFalse(authenticationFailure);

        }
        //[Test]
        //public void TempGetPasswordHash()
        //{
        //    LoginService service = new LoginService(null);
        //    string salt = service.CreateSalt();
        //    string password = "password";
        //    string hash = service.CreatePasswordHash(password, salt);
        //}
    }
}
