using System.Web.Routing;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using CodeCampServer.Website.Views;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using System.Security;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class LoginControllerTester
	{
		private MockRepository _mocks;
	    private IPersonRepository _personRepository;
		private ILoginService _loginService;
		private IAuthenticationService _authenticationService;
	    private IAuthorizationService _authorizationService;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_loginService = _mocks.CreateMock<ILoginService>();
			_authenticationService = _mocks.DynamicMock<IAuthenticationService>();
		    _authorizationService = _mocks.DynamicMock<IAuthorizationService>();
		    _personRepository = _mocks.DynamicMock<IPersonRepository>();
		}

		private class TestingLoginController : LoginController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;
			public string RedirectUrl;
		    public RouteValueDictionary RedirectToActionValues;

			public TestingLoginController(ILoginService loginService, IPersonRepository personRepository, IAuthenticationService authenticationService, IAuthorizationService authorizationService)
				: base(loginService, personRepository, authenticationService, authorizationService)
			{
			}

			public override void Redirect(string url)
			{
				RedirectUrl = url;
			}

			protected override void RenderView(string viewName,
			                                   string masterName,
			                                   object viewData)
			{
				ActualViewName = viewName;
				ActualMasterName = masterName;
				ActualViewData = viewData;
			}

            protected override void RedirectToAction(RouteValueDictionary values)
            {
                RedirectToActionValues = values;   
            }
		}

		[Test]
		public void LoginActionShouldRenderIndexView()
		{
			TestingLoginController controller = new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
			controller.Index();

			Assert.That(controller.ActualViewName, Is.EqualTo("loginform"));
		}

        [Test]
        public void LoginActionShouldCheckNumberOfRegisteredUsers()
    	{
            Expect.Call(_personRepository.GetNumberOfUsers()).Return(44);
            _mocks.ReplayAll();

	        TestingLoginController controller = new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
            controller.Index();

            _mocks.VerifyAll();
	    }

	    [Test]
	    public void LoginActionShouldSetFirstTimeRegisterWhenNoUsersArePresent()
	    {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);
            _mocks.ReplayAll();

            TestingLoginController controller = new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
            controller.Index();

	        Assert.That(((SmartBag) controller.ActualViewData).Get<bool>("ShowFirstTimeRegisterLink"), Is.True);
	    }

        [Test]
        public void LoginActionShouldNotSetFirstTimeRegisterWhenUsersArePresent()
        {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(1);
            _mocks.ReplayAll();

            TestingLoginController controller = GetController();
            controller.Index();

            Assert.That(((SmartBag)controller.ActualViewData).Get<bool>("ShowFirstTimeRegisterLink"), Is.False);
        }

	    [Test, ExpectedException(typeof(SecurityException))]
	    public void CreateAdminAccountThrowsSecurityErrorIfCalledWhenUsersArePresent()
	    {
	        SetupResult.For(_personRepository.GetNumberOfUsers()).Return(1);
	        _mocks.ReplayAll();

	        LoginController controller = GetController();
	        controller.CreateAdminAccount("test", "user", "email@email.com", "pwd", "pwd");
	    }


        public void CreateAdminAccountVerifiesEmailAndPasswordExist()
        {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);
            _mocks.ReplayAll();

            TestingLoginController controller = GetController();
            controller.CreateAdminAccount("fname", "lname", null, null, null);

            Assert.That(controller.TempData["error"], Is.Not.Null);
            Assert.That(controller.RedirectToActionValues["action"], Is.EqualTo("index"));
        }

        private TestingLoginController GetController()
        {
            return new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
        }

		[Test]
		public void ProcessLoginShouldRedirectToReturnUrlOnSuccess()
		{
            TestingLoginController controller = new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
			string email = "brownie@brownie.com.au";
			string password = "nothing";
			string returnUrl = "http://testurl/";
			SetupResult.For(_loginService.VerifyAccount(email, password)).Return(true);
			_mocks.ReplayAll();

			controller.Process(email, password, returnUrl);
			Assert.That(controller.RedirectUrl, Is.EqualTo(returnUrl));
		}

		[Test]
		public void ProcessLoginShouldRenderLoginFailureOnFailure()
		{
            TestingLoginController controller = new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
			string email = "brownie@brownie.com.au";
			string password = "nothing";
			SetupResult.For(_loginService.VerifyAccount(email, password)).Return(false);
			_mocks.ReplayAll();
			controller.Process(email, password, "");
			Assert.That(controller.ActualViewName, Is.EqualTo("loginfailed"));
		}

        [Test]
        public void ProcessLoginShouldRedirectToDefaultPageOnSuccessAndNullReturnUrl()
        {
            TestingLoginController controller = new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
            SetupResult.For(_loginService.VerifyAccount(null, null))
                .IgnoreArguments()
                .Return(true);
            _mocks.ReplayAll();

            controller.Process("brownie@brownie.com.au", "password", null);
            Assert.That(controller.RedirectUrl, Is.EqualTo("~/default.aspx"));
        }
	}
}