using CodeCampServer.Model.Security;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class ApplicationControllerTests
    {
        IAuthorizationService _authorizationService;       

        private class FooController : Controller
        {
            public FooController(IAuthorizationService authorizationService) : base(authorizationService)
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
            _authorizationService = _mocks.CreateMock<IAuthorizationService>();
        }

        [Test]
        public void OnPreActionShouldVerifyTheUserIsAnAdmin()
        {
            Expect.Call(_authorizationService.IsAdministrator).Return(true);
            _mocks.ReplayAll();

            var foo = new FooController(_authorizationService);
            foo.Bar();

            _mocks.VerifyAll();
        }

        [Test]
        public void OnPreActionShouldSetSmartBagDataToRenderAdminPanelWhenTheUserIsAnAdmin()
        {
            SetupResult.For(_authorizationService.IsAdministrator).Return(true);
            _mocks.ReplayAll();
            var foo = new FooController(_authorizationService);
            foo.Bar();

            Assert.That(foo.ViewData.ContainsKey("ShouldRenderAdminPanel"));
        }
    }
}
