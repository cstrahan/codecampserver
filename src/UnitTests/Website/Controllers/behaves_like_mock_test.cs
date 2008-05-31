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
            _mocks = new MockRepository();
        }

        [TearDown]
        public virtual void TearDown()
        {
            _mocks.VerifyAll();
        }
    }
}