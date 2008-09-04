using CodeCampServer.Model;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    [TestFixture]
    public class AdminControllerTester
    {
        private IUserSession _userSession;

        [SetUp]
        public void SetUp()
        {            
            _userSession = MockRepository.GenerateStub<IUserSession>();
        }

        [Test]
        public void IndexActionRendersDefaultView()
        {
            var controller = getController();
            controller.Index().ShouldRenderDefaultView();            
        }

        private AdminController getController()
        {
            var controller = new AdminController(_userSession);                  
            controller.ControllerContext = new ControllerContextStub(controller);
            return controller;
        }

    }
}