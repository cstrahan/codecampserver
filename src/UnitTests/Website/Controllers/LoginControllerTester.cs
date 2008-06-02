using System.Security;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class LoginControllerTester
    {
        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
            _loginService = _mocks.CreateMock<ILoginService>();
            _authenticationService = _mocks.DynamicMock<IAuthenticationService>();
            _authorizationService = _mocks.DynamicMock<IAuthorizationService>();
            _personRepository = _mocks.DynamicMock<IPersonRepository>();
            _cryptoUtil = _mocks.DynamicMock<ICryptoUtil>();
            _tempData = new TempDataDictionary(_mocks.FakeHttpContext("~/login"));
        }

        private MockRepository _mocks;
        private IPersonRepository _personRepository;
        private ILoginService _loginService;
        private IAuthenticationService _authenticationService;
        private IAuthorizationService _authorizationService;
        private TempDataDictionary _tempData;
        private ICryptoUtil _cryptoUtil;

        private LoginController getController()
        {
            return new LoginController(_loginService, _personRepository, _authenticationService, _authorizationService,
                                       _cryptoUtil)
                       {
                           TempData = _tempData
                       };
        }

        [Test]
        public void CreateAdminAccountSetsErrorMessageAndRedirectsBackToIndexIfEmailOrPasswordIsNotSet()
        {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);

            LoginController controller = getController();
            _mocks.ReplayAll();

            var actionResult = controller.CreateAdminAccount("fname", "lname", null, null, null) as RedirectToRouteResult;

            if(actionResult == null) 
                Assert.Fail("expected action redirect result");
            Assert.That(controller.TempData["error"], Is.Not.Null);
            Assert.That(actionResult, Is.Not.Null, "should have redirected");
            Assert.That(actionResult.Values["action"], Is.EqualTo("index"));
        }

        [Test, ExpectedException(typeof (SecurityException))]
        public void CreateAdminAccountThrowsSecurityErrorIfCalledWhenUsersArePresent()
        {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(1);
            var controller = getController();
            _mocks.ReplayAll();

            controller.CreateAdminAccount("test", "user", "email@email.com", "pwd", "pwd");
        }

        [Test]
        public void LoginActionShouldCheckNumberOfRegisteredUsers()
        {
            LoginController controller = getController();
            Expect.Call(_personRepository.GetNumberOfUsers()).Return(44);
            _mocks.ReplayAll();

            controller.Index();

            _mocks.VerifyAll();
        }

        [Test]
        public void LoginActionShouldNotSetFirstTimeRegisterWhenUsersArePresent()
        {
            LoginController controller = getController();
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(1);
            _mocks.ReplayAll();

            controller.Index();

            Assert.That(controller.ViewData.Get<bool>("ShowFirstTimeRegisterLink"), Is.False);
        }

        [Test]
        public void LoginActionShouldRenderIndexView()
        {
            var controller = getController();
            var actionResult = controller.Index() as ViewResult;

            if(actionResult == null) Assert.Fail("a view should have been rendered");
            Assert.That(actionResult.ViewName, Is.EqualTo("loginform"));
        }

        [Test]
        public void LoginActionShouldSetFirstTimeRegisterWhenNoUsersArePresent()
        {
            LoginController controller = getController();
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);
            _mocks.ReplayAll();

            controller.Index();

            Assert.That(controller.ViewData.Get<bool>("ShowFirstTimeRegisterLink"), Is.True);
        }

        [Test]
        public void ProcessLoginShouldRedirectToDefaultPageOnSuccessAndNullReturnUrl()
        {
            SetupResult.For(_loginService.VerifyAccount(null, null))
                .IgnoreArguments()
                .Return(true);
            LoginController controller = getController();
            _mocks.ReplayAll();

            var actionResult = controller.Process("brownie@brownie.com.au", "password", null) as RedirectToRouteResult;
            
            if(actionResult == null) Assert.Fail("expected RedirectToRouteResult");
            
            Assert.That(actionResult.Values["controller"].ToString().ToLower(), Is.EqualTo("conference"));
            Assert.That(actionResult.Values["action"].ToString().ToLower(), Is.EqualTo("current"));
        }

        [Test]
        public void ProcessLoginShouldRedirectToIndexOnFailureWithError()
        {
            const string email = "brownie@brownie.com.au";
            const string password = "nothing";
            SetupResult.For(_loginService.VerifyAccount(email, password)).Return(false);
            LoginController controller = getController();

            _mocks.ReplayAll();

            var actionResult = controller.Process(email, password, "") as RedirectToRouteResult;

            if(actionResult == null) Assert.Fail("should have redirected to an action");
            Assert.That(actionResult.Values["action"], Is.EqualTo("index"));
            Assert.That(controller.TempData[TempDataKeys.Error], Is.Not.Null);
        }

        [Test]
        public void ProcessLoginShouldRedirectToReturnUrlOnSuccess()
        {
            const string email = "brownie@brownie.com.au";
            const string password = "nothing";
            const string returnUrl = "http://testurl/";
            SetupResult.For(_loginService.VerifyAccount(email, password)).Return(true);
            var controller = getController();
            _mocks.ReplayAll();

            var actionResult = controller.Process(email, password, returnUrl) as UrlRedirectResult;

            if(actionResult == null) Assert.Fail("should have redirected to a url");
            Assert.That(actionResult.Url, Is.EqualTo(returnUrl));
        }
    }

    [TestFixture]
    public class when_logging_out : behaves_like_login_controller_test
    {
        public override void Setup()
        {
            base.Setup();
            Expect.Call(() => _authenticationService.SignOut());
            _mocks.ReplayAll();
        }

        [Test]
        public void should_sign_out_from_authentication_service()
        {            
            _loginController.Logout();
        }

        [Test]
        public void should_redirect_to_home_page()
        {
            var result = _loginController.Logout() as RedirectToRouteResult;
            if(result == null)
                Assert.Fail("Expected a redirect result");
            
            Assert.That(result.Values["controller"], Is.EqualTo("home"));
            Assert.That(result.Values["action"], Is.EqualTo("index"));
        }
    }

    public class behaves_like_login_controller_test : behaves_like_mock_test
    {
        protected IAuthenticationService _authenticationService;
        protected LoginController _loginController;

        public override void Setup()
        {
            base.Setup();
            _authenticationService = _mocks.DynamicMock<IAuthenticationService>();

            _loginController = new LoginController(_mocks.Stub<ILoginService>(), 
                _mocks.Stub<IPersonRepository>(), _authenticationService, 
                _mocks.Stub<IAuthorizationService>(), _mocks.Stub<ICryptoUtil>());
        }
    }
}