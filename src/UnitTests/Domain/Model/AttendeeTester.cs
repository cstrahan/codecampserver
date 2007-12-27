using CodeCampServer.Model.Domain;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Domain.Model
{
    [TestFixture]
    public class AttendeeTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Attendee();
        }
    }
}