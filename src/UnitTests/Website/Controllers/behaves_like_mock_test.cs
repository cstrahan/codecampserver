using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
    public class behaves_like_mock_test
    {
        protected MockRepository _mocks;

        [SetUp]
        public virtual void Setup()
        {
        }

        [TearDown]
        public virtual void TearDown()
        {
        }

        protected T Mock<T>()
        {
            return MockRepository.GenerateMock<T>();
        }

        protected T Stub<T>()
        {
            return MockRepository.GenerateStub<T>();
        }        
    }
}