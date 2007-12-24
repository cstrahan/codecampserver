using CodeCampServer.Domain.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Domain.Model
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