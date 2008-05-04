using System;
using CodeCampServer.Model.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Domain
{
    [TestFixture]
    public class PersonTester
    {
        [Test, Ignore]
        public void PersonShouldTestEqualityBasedOnNonEmptyId()
        {
            var p = new Person();
            var p2 = new Person();

            Assert.That(p, Is.EqualTo(p));
            Assert.That(p.GetHashCode(), Is.EqualTo(p.GetHashCode()));

            Assert.That(p, Is.Not.EqualTo(p2));
            Assert.That(p.GetHashCode(), Is.Not.EqualTo(p2.GetHashCode()));

            p.Id = Guid.NewGuid();
            p2.Id = p.Id;

            Assert.That(p, Is.EqualTo(p2));
            Assert.That(p.GetHashCode(), Is.EqualTo(p2.GetHashCode()));
        }
    }
}
