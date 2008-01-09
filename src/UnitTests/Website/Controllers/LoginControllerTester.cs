using System;
using System.Collections;
using System.Collections.Generic;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Presentation;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using CodeCampServer.Model.Security;

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

			public TestingLoginController(ILoginService loginService, IAuthenticationService authenticationService)
				: base(loginService, authenticationService)
        	{
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
        public void LoginActionShouldRenderLoginView()
        {
           TestingLoginController controller = new TestingLoginController(_loginService, _authenticationService);
           controller.Login();

           Assert.That(controller.ActualViewName, Is.EqualTo("loginform"));
        }

        [Test]
        public void ProcessLoginShouldRenderLoginSuccessOnSuccess()
        {
			TestingLoginController controller = new TestingLoginController(_loginService, _authenticationService);
            string email = "brownie@brownie.com.au";
            string password = "nothing";
            SetupResult.For(_loginService.VerifyAccount(email, password)).Return(true);
            _mocks.ReplayAll();
            controller.ProcessLogin(email, password);
            Assert.That(controller.ActualViewName, Is.EqualTo("loginsuccess"));
        }

        [Test]
        public void ProcessLoginShouldRenderLoginFailureOnFailure()
        {
            TestingLoginController controller = new TestingLoginController(_loginService, _authenticationService);
            string email = "brownie@brownie.com.au";
            string password = "nothing";
            SetupResult.For(_loginService.VerifyAccount(email, password)).Return(false);
            _mocks.ReplayAll();
            controller.ProcessLogin(email, password);
            Assert.That(controller.ActualViewName, Is.EqualTo("loginfailed"));
        }
    }
}
