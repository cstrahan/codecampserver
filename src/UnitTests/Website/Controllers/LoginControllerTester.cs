using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class LoginControllerTester
	{
		private MockRepository _mocks;
		private ILoginService _loginService;
		private IAuthenticationService _authenticationService;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_loginService = _mocks.CreateMock<ILoginService>();
			_authenticationService = _mocks.DynamicMock<IAuthenticationService>();
		}

		private class TestingLoginController : LoginController
		{
			public string ActualViewName;
			public string ActualMasterName;
			public object ActualViewData;
			public string RedirectUrl;

			public TestingLoginController(ILoginService loginService, IAuthenticationService authenticationService)
				: base(loginService, authenticationService)
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
		}

		[Test]
		public void LoginActionShouldRenderIndexView()
		{
			TestingLoginController controller = new TestingLoginController(_loginService, _authenticationService);
			controller.Index();

			Assert.That(controller.ActualViewName, Is.EqualTo("loginform"));
		}

		[Test]
		public void ProcessLoginShouldRedirectToReturnUrlOnSuccess()
		{
			TestingLoginController controller = new TestingLoginController(_loginService, _authenticationService);
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
			TestingLoginController controller = new TestingLoginController(_loginService, _authenticationService);
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
            TestingLoginController controller = new TestingLoginController(_loginService, _authenticationService);
            SetupResult.For(_loginService.VerifyAccount(null, null))
                .IgnoreArguments()
                .Return(true);
            _mocks.ReplayAll();

            controller.Process("brownie@brownie.com.au", "password", null);
            Assert.That(controller.RedirectUrl, Is.EqualTo("~/default.aspx"));
        }
	}
}