using CodeCampServer.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Domain.Model
{
    [TestFixture]
    public class SessionTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Session();
        }
    }
}