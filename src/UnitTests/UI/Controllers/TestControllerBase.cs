using CodeCampServer.Core.Domain;
using CodeCampServer.UI.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.UI.Controllers
{
    [TestFixture]
    public abstract class TestControllerBase<TController>
    {
        [SetUp]
        public void SetUp()
        {
            this.controllerUnderTest = CreateController();
        }
        protected TController controllerUnderTest;
        public const string DEFAULT_VIEW = "";

        /// <summary>
        /// Creates a Mock of type T
        /// </summary>
        public T Mock<T>(params object[] argumentsForConstructor) where T : class
        {
            return MockRepository.GenerateMock<T>(argumentsForConstructor);
        }

        /// <summary>
        /// Creates a Stub of type T
        /// </summary>
        public T Stub<T>(params object[] argumentsForConstructor) where T : class
        {
            return MockRepository.GenerateStub<T>(argumentsForConstructor);
        }

        protected abstract TController CreateController();
    }
}