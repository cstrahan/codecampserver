using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
using CodeCampServer.Website;
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
	    private TempDataDictionary _tempData;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_loginService = _mocks.CreateMock<ILoginService>();
			_authenticationService = _mocks.DynamicMock<IAuthenticationService>();
		    _authorizationService = _mocks.DynamicMock<IAuthorizationService>();
		    _personRepository = _mocks.DynamicMock<IPersonRepository>();
            _tempData = new TempDataDictionary(_mocks.FakeHttpContext("~/login"));
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

		    public IDictionary<string, object> ActualViewDataAsDictionary
		    {
                get { return (IDictionary<string, object>) ActualViewData; }
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

	    private TestingLoginController getController()
	    {
	        var controller = new TestingLoginController(_loginService, _personRepository, _authenticationService, _authorizationService);
	        controller.TempData = _tempData;

	        return controller;
	    }

	    [Test]
		public void LoginActionShouldRenderIndexView()
		{
			var controller = getController();
			controller.Index();

			Assert.That(controller.ActualViewName, Is.EqualTo("loginform"));
		}

	    [Test]
        public void LoginActionShouldCheckNumberOfRegisteredUsers()
    	{
            var controller = getController();
            Expect.Call(_personRepository.GetNumberOfUsers()).Return(44);
            
            _mocks.ReplayAll();
	        
            controller.Index();

            _mocks.VerifyAll();
	    }

	    [Test]
	    public void LoginActionShouldSetFirstTimeRegisterWhenNoUsersArePresent()
	    {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);
            var controller = getController();
            _mocks.ReplayAll();
	        
            controller.Index();

	        Assert.That(controller.ActualViewDataAsDictionary.Get<bool>("ShowFirstTimeRegisterLink"), Is.True);
	    }

        [Test]
        public void LoginActionShouldNotSetFirstTimeRegisterWhenUsersArePresent()
        {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(1);
            var controller = getController();
            _mocks.ReplayAll();
            
            controller.Index();

            Assert.That(controller.ActualViewDataAsDictionary.Get<bool>("ShowFirstTimeRegisterLink"), Is.False);
        }

	    [Test, ExpectedException(typeof(SecurityException))]
	    public void CreateAdminAccountThrowsSecurityErrorIfCalledWhenUsersArePresent()
	    {
	        SetupResult.For(_personRepository.GetNumberOfUsers()).Return(1);
            var controller = getController();
	        _mocks.ReplayAll();
	        
	        controller.CreateAdminAccount("test", "user", "email@email.com", "pwd", "pwd");
	    }

        [Test]
        public void CreateAdminAccountVerifiesEmailAndPasswordExist()
        {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);

            var controller = getController();
            _mocks.ReplayAll();
            
            controller.CreateAdminAccount("fname", "lname", null, null, null);

            Assert.That(controller.TempData["error"], Is.Not.Null);
            Assert.That(controller.RedirectToActionValues["action"], Is.EqualTo("index"));
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

			controller.Process(email, password, returnUrl);
			Assert.That(controller.RedirectUrl, Is.EqualTo(returnUrl));
		}

		[Test]
		public void ProcessLoginShouldRedirectToIndexOnFailureWithError()
		{
		    const string email = "brownie@brownie.com.au";
		    const string password = "nothing";
		    SetupResult.For(_loginService.VerifyAccount(email, password)).Return(false);
		    var controller = getController();
		    
            _mocks.ReplayAll();
			
            controller.Process(email, password, "");

            Assert.That(controller.RedirectToActionValues["action"], Is.EqualTo("index"));
			Assert.That(controller.TempData[TempDataKeys.Error], Is.Not.Null);
		}

        [Test]
        public void ProcessLoginShouldRedirectToDefaultPageOnSuccessAndNullReturnUrl()
        {
            SetupResult.For(_loginService.VerifyAccount(null, null))
                .IgnoreArguments()
                .Return(true);
            var controller = getController();
            _mocks.ReplayAll();

            controller.Process("brownie@brownie.com.au", "password", null);
            Assert.That(controller.RedirectUrl, Is.EqualTo("~/default.aspx"));
        }
	}
}