using CodeCampServer.Model;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class ApplicationControllerTester
	{
		private IUserSession _userSession;

		private class FooController : Controller
		{
			public FooController(IUserSession userSession) : base(userSession)
			{
			}

			public void Bar()
			{
				//would normally be called by the Execute method
				OnActionExecuting(null);
			}
		}

		private MockRepository _mocks;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_userSession = _mocks.CreateMock<IUserSession>();
		}

		[Test]
		public void OnPreActionShouldVerifyTheUserIsAnAdmin()
		{
			Expect.Call(_userSession.IsAdministrator).Return(true);
			_mocks.ReplayAll();

			var foo = new FooController(_userSession);
			foo.Bar();

			_mocks.VerifyAll();
		}

		[Test]
		public void OnPreActionShouldSetViewDataToRenderAdminPanelWhenTheUserIsAnAdmin()
		{
			SetupResult.For(_userSession.IsAdministrator).Return(true);
			_mocks.ReplayAll();
			var foo = new FooController(_userSession);
			foo.Bar();

			Assert.That(foo.ViewData.ContainsKey("ShouldRenderAdminPanel"));
		}
	}
}