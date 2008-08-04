using System;
using System.Security;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Security;
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
		private IPersonRepository _personRepository;
		private IUserSession _userSession;
		private IAuthenticator _authenticator;
		private ICryptographer _cryptographer;

		[SetUp]
		public void Setup()
		{
			_userSession = MockRepository.GenerateMock<IUserSession>();
			_authenticator = MockRepository.GenerateStub<IAuthenticator>();
			_personRepository = MockRepository.GenerateStub<IPersonRepository>();
			_cryptographer = MockRepository.GenerateStub<ICryptographer>();
		}

		private LoginController getController()
		{
			return new LoginController(_userSession, _personRepository, _authenticator,
			                           _cryptographer);
		}

		[Test]
		public void CreateAdminAccountSetsErrorMessageAndRedirectsBackToIndexIfEmailOrPasswordIsNotSet()
		{
			_personRepository.Stub(r => r.GetNumberOfUsers()).Return(0);
			_userSession.Expect(s => s.PushUserMessage(FlashMessage.MessageType.Error, "Email and Password are required"));

			LoginController controller = getController();

			var actionResult = controller.CreateAdminAccount("fname", "lname", null, null, null) as RedirectToRouteResult;

			if (actionResult == null)
				Assert.Fail("expected action redirect result");

			Assert.That(actionResult, Is.Not.Null, "should have redirected");
			Assert.That(actionResult.Values["action"], Is.EqualTo("index"));
			_userSession.VerifyAllExpectations();
		}

		[Test, ExpectedException(typeof (SecurityException))]
		public void CreateAdminAccountThrowsSecurityErrorIfCalledWhenUsersArePresent()
		{
			_personRepository.Stub(r => r.GetNumberOfUsers()).Return(1);
			LoginController controller = getController();

			controller.CreateAdminAccount("test", "user", "email@email.com", "pwd", "pwd");
		}

		[Test]
		public void LoginActionShouldNotSetFirstTimeRegisterWhenUsersArePresent()
		{
			LoginController controller = getController();
			_personRepository.Stub(r => r.GetNumberOfUsers()).Return(1);

			controller.Index();

			Assert.That(controller.ViewData.Get<bool>("ShowFirstTimeRegisterLink"), Is.False);
		}

		[Test]
		public void LoginActionShouldRenderIndexView()
		{
			LoginController controller = getController();
			var actionResult = controller.Index() as ViewResult;

			if (actionResult == null) Assert.Fail("a view should have been rendered");
			Assert.That(actionResult.ViewName, Is.EqualTo("loginform"));
		}

		[Test]
		public void LoginActionShouldSetFirstTimeRegisterWhenNoUsersArePresent()
		{
			LoginController controller = getController();
			_personRepository.Stub(r => r.GetNumberOfUsers()).Return(0);

			controller.Index();

			Assert.That(controller.ViewData.Get<bool>("ShowFirstTimeRegisterLink"), Is.True);
		}

		[Test]
		public void ProcessLoginShouldRedirectToDefaultPageOnSuccessAndNullReturnUrl()
		{
			_personRepository.Stub(r => r.FindByEmail("brownie@brownie.com.au")).Return(new Person());
			_authenticator.Stub(a => a.VerifyAccount(null, null))
				.IgnoreArguments()
				.Return(true);
			LoginController controller = getController();

			var actionResult = controller.Process("brownie@brownie.com.au", "password", null) as RedirectToRouteResult;

			if (actionResult == null) Assert.Fail("expected RedirectToRouteResult");

			Assert.That(actionResult.Values["controller"].ToString().ToLower(), Is.EqualTo("conference"));
			Assert.That(actionResult.Values["action"].ToString().ToLower(), Is.EqualTo("current"));
		}

		[Test]
		public void ProcessLoginShouldRedirectToIndexOnFailureWithError()
		{
			const string email = "brownie@brownie.com.au";
			const string password = "nothing";
			var person = new Person();
			_personRepository.Stub(r => r.FindByEmail("brownie@brownie.com.au")).Return(person);
			_authenticator.Stub(r => r.VerifyAccount(person, password)).Return(false);
			_userSession.Expect(s => s.PushUserMessage(FlashMessage.MessageType.Error, "Invalid login"));

			LoginController controller = getController();

			var actionResult = controller.Process(email, password, "") as RedirectToRouteResult;

			if (actionResult == null) Assert.Fail("should have redirected to an action");
			Assert.That(actionResult.Values["action"], Is.EqualTo("index"));

			_userSession.VerifyAllExpectations();
		}

		[Test]
		public void ProcessLoginShouldRedirectToReturnUrlOnSuccess()
		{
			const string email = "brownie@brownie.com.au";
			const string password = "nothing";
			const string returnUrl = "http://testurl/";
			var person = new Person();
			_personRepository.Stub(r => r.FindByEmail("brownie@brownie.com.au")).Return(person);
			_authenticator.Stub(r => r.VerifyAccount(person, password)).Return(true);
			LoginController controller = getController();

			var actionResult = controller.Process(email, password, returnUrl) as UrlRedirectResult;

			if (actionResult == null) Assert.Fail("should have redirected to a url");
			Assert.That(actionResult.Url, Is.EqualTo(returnUrl));
		}
	}

	[TestFixture]
	public class when_logging_out : behaves_like_login_controller_test
	{
		public override void Setup()
		{
			base.Setup();
			Expect.Call(() => _authenticator.SignOut());
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
			if (result == null)
				Assert.Fail("Expected a redirect result");

			Assert.That(result.Values["controller"], Is.EqualTo("conference"));
			Assert.That(result.Values["action"], Is.EqualTo("current"));
		}
	}

	public class behaves_like_login_controller_test : behaves_like_mock_test
	{
		protected IAuthenticator _authenticator;
		protected LoginController _loginController;

		public override void Setup()
		{
			base.Setup();
			_authenticator = _mocks.DynamicMock<IAuthenticator>();

			_loginController = new LoginController(_mocks.Stub<IUserSession>(),
			                                       _mocks.Stub<IPersonRepository>(), _authenticator,
			                                       _mocks.Stub<ICryptographer>());
		}
	}
}