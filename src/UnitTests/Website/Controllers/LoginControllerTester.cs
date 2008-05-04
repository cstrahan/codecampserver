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
	    private ICryptoUtil _cryptoUtil;

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

	    [Test]
		public void LoginActionShouldRenderIndexView()
		{
			var controller = getController();
	        var actionResult = controller.Index() as RenderViewResult;

            Assert.That(actionResult, Is.Not.Null, "a view should have been rendered");
	        Assert.That(actionResult.ViewName, Is.EqualTo("loginform"));
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
	        var controller = getController();
	        SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);
	        _mocks.ReplayAll();

	        controller.Index();

	        Assert.That(controller.ViewData.Get<bool>("ShowFirstTimeRegisterLink"), Is.True);
	    }

	    [Test]
        public void LoginActionShouldNotSetFirstTimeRegisterWhenUsersArePresent()
        {
	        var controller = getController();
	        SetupResult.For(_personRepository.GetNumberOfUsers()).Return(1);
	        _mocks.ReplayAll();
            
            controller.Index();

            Assert.That(controller.ViewData.Get<bool>("ShowFirstTimeRegisterLink"), Is.False);
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
        public void CreateAdminAccountSetsErrorMessageAndRedirectsBackToIndexIfEmailOrPasswordIsNotSet()
        {
            SetupResult.For(_personRepository.GetNumberOfUsers()).Return(0);

            var controller = getController();
            _mocks.ReplayAll();

	        var actionResult = controller.CreateAdminAccount("fname", "lname", null, null, null) as ActionRedirectResult;

	        Assert.That(controller.TempData["error"], Is.Not.Null);
	        Assert.That(actionResult, Is.Not.Null, "should have redirected");
	        Assert.That(actionResult.Values["action"], Is.EqualTo("index"));
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

            Assert.That(actionResult, Is.Not.Null, "should have redirected to a url");
	        Assert.That(actionResult.Url, Is.EqualTo(returnUrl));
		}

	    [Test]
		public void ProcessLoginShouldRedirectToIndexOnFailureWithError()
		{
		    const string email = "brownie@brownie.com.au";
		    const string password = "nothing";
		    SetupResult.For(_loginService.VerifyAccount(email, password)).Return(false);
		    var controller = getController();
		    
            _mocks.ReplayAll();

	        var actionResult = controller.Process(email, password, "") as ActionRedirectResult;

            Assert.That(actionResult, Is.Not.Null, "should have redirected to an action");
	        Assert.That(actionResult.Values["action"], Is.EqualTo("index"));
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

	        var actionResult = controller.Process("brownie@brownie.com.au", "password", null) as ActionRedirectResult;
            Assert.That(actionResult, Is.Not.Null, "should have redirected to a url");
	        Assert.That(actionResult.Values["controller"].ToString().ToLower(), Is.EqualTo("conference"));
	        Assert.That(actionResult.Values["action"].ToString().ToLower(), Is.EqualTo("current"));
        }

	    private LoginController getController()
	    {
	        return new LoginController(_loginService, _personRepository, _authenticationService, _authorizationService,
	                                   _cryptoUtil)
	                   {
	                       TempData = _tempData
                       };	        
	    }
	}
}