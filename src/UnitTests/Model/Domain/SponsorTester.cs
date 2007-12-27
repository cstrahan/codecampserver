using CodeCampServer.Model.Domain;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Model.Domain
{
    [TestFixture]
    public class SponsorTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Sponsor();
        }
    }
}