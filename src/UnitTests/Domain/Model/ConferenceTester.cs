using CodeCampServer.Domain.Model;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Domain.Model
{
    [TestFixture]
    public class ConferenceTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Conference();
        }
    }
}