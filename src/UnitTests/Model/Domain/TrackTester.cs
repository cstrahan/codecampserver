using System;
using CodeCampServer.Model.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Domain
{
    [TestFixture]
    public class TrackTester : EntityTesterBase
    {
        protected override EntityBase createEntity()
        {
            return new Track();
        }
    }
}
