using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class AdminControllerTester
    {
        private MockRepository _mocks;
        private IUserSession userSession;

        [SetUp]
        public void SetUp()
        {
            _mocks = new MockRepository();
            userSession = _mocks.DynamicMock<IUserSession>();
        }

        [Test]
        public void IndexActionRendersDefaultView()
        {
            var controller = getController();
            var result = controller.Index() as ViewResult;

            if(result == null) 
                Assert.Fail("expected renderview");

            Assert.That(result.ViewName, Is.Null);
        }

        private AdminController getController()
        {
            var controller = new AdminController(userSession)
                       {                           
                           ViewEngine = new ViewEngineStub()
                       };

            controller.ControllerContext = new ControllerContextStub(controller);
            return controller;
        }

    }
}