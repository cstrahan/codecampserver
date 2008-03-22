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

        private class FooController : ApplicationController
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


        private class RenderTestController : ApplicationController
        {
            public object ActualViewData;

            public RenderTestController(IAuthorizationService authorizationService) 
                : base(authorizationService)
            {
            }

            protected override void RenderView(string viewName, string masterName, object viewData)
            {
                ActualViewData = viewData;
            }

            public void RenderWithViewName(string viewName)
            {
                RenderView(viewName);
            }

            public void RenderWithViewNameAndMasterName(string viewName, string masterName)
            {
                RenderView(viewName, masterName);
            }
        }
        
        [Test]
        public void RenderWithViewStringShouldUseSmartBagAndNotDefaultViewData()
        {
            RenderTestController controller = new RenderTestController(_authorizationService);
            controller.RenderWithViewName("TestView");

            Assert.That(controller.ActualViewData, Is.Null);
        }

        [Test]
        public void RenderWithViewStringAndMasterNameShouldUseSmartBagAndNotDefaultViewData()
        {
            RenderTestController controller = new RenderTestController(_authorizationService);
            controller.RenderWithViewNameAndMasterName("TestView", "Master");

            Assert.That(controller.ActualViewData, Is.Null);
        }
    }


}
