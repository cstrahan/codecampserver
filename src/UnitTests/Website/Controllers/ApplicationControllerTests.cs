using System.Web.Mvc;
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

            [ControllerAction]
            public void Bar()
            {
                //would normally be called by the Execute method
                OnPreAction("bar", null);
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

            FooController controller = new FooController(_authorizationService);            
            controller.Bar();
            
            _mocks.VerifyAll();
        }

        [Test]
        public void OnPreActionShouldSetSmartBagDataToRenderAdminPanelWhenTheUserIsAnAdmin()
        {
            SetupResult.For(_authorizationService.IsAdministrator).Return(true);
            _mocks.ReplayAll();

            FooController controller = new FooController(_authorizationService);
            controller.Bar();

            Assert.That(controller.SmartBag.ContainsKey("ShouldRenderAdminPanel"));
            Assert.That(controller.SmartBag.Get<bool>("ShouldRenderAdminPanel"), Is.True);

            _mocks.BackToRecordAll();
            SetupResult.For(_authorizationService.IsAdministrator).Return(false);
            _mocks.ReplayAll();

            controller.Bar();
            _mocks.VerifyAll();
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

            [ControllerAction]
            public void RenderWithViewName(string viewName)
            {
                RenderView(viewName);
            }
            [ControllerAction]
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
