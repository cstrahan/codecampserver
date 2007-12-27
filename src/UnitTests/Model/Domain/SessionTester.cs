using CodeCampServer.Model.Domain;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Model.Domain
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