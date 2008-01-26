using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeCampServer.Model.Security;
using NUnit.Framework;
using CodeCampServer.Website.Controllers;
using System.Web.Mvc;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using System.Reflection;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class ApplicationControllerTests
    {
        private class FooController : ApplicationController
        {
            public FooController(IAuthorizationService authorizationService) : base(authorizationService)
            {
            }

            [ControllerAction]
            public void Bar()
            {
                //would normally be called by the Execute method
                this.OnPreAction("bar", null);
            }
        }

        private MockRepository _mocks;

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();
        }

        [Test]
        public void OnPreActionShouldVerifyTheUserIsAnAdmin()
        {
            IAuthorizationService mockAuthorizationService = _mocks.CreateMock<IAuthorizationService>();
            Expect.Call(mockAuthorizationService.IsAdministrator).Return(true);

            _mocks.ReplayAll();

            FooController controller = new FooController(mockAuthorizationService);            
            controller.Bar();
            
            _mocks.VerifyAll();
        }

        [Test]
        public void OnPreActionShouldSetSmartBagDataToRenderAdminPanelWhenTheUserIsAnAdmin()
        {
            IAuthorizationService mockAuthorizationService = _mocks.DynamicMock<IAuthorizationService>();
            SetupResult.For(mockAuthorizationService.IsAdministrator).Return(true);

            _mocks.ReplayAll();

            FooController controller = new FooController(mockAuthorizationService);
            controller.Bar();

            Assert.That(controller.SmartBag.ContainsKey("ShouldRenderAdminPanel"));
            Assert.That(controller.SmartBag.Get<bool>("ShouldRenderAdminPanel"), Is.True);

            _mocks.BackToRecordAll();
            SetupResult.For(mockAuthorizationService.IsAdministrator).Return(false);
            _mocks.ReplayAll();

            controller.Bar();
            _mocks.VerifyAll();
        }
    }


}
