using CodeCampServer.Model.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Domain
{
    public abstract class EntityTesterBase
    {
        [Test]
        public void WhenIdIsEmptyAndObjectsAreSame_ShouldBeEqual()
        {
            EntityBase entity = createEntity();
            EntityBase entity2 = createEntity();

            Assert.That(entity, Is.Not.EqualTo(entity2));
        }

        protected abstract EntityBase createEntity();
    }
}