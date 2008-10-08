using CodeCampServer.Model.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Domain
{
    [TestFixture]
    public class SpeakerTester
    {
        [Test]
        public void ShouldBeEqualIfAllPropertiesAreEqual()
        {
            Person person = new Person("Barney", "Rubble", "brubble@gmail.com");
            string bio = "test bio";
            string avatarurl = "me.jpg";
            string speakerKey = "key";
            Speaker speaker = new Speaker(person, speakerKey, bio, avatarurl);
            Speaker speaker2 = new Speaker(person, speakerKey, bio, avatarurl);

            Assert.That(speaker, Is.EqualTo(speaker2));
        }

        [Test]
        public void ShouldNOTBeEqualIfPropertiesAreNotEqual()
        {
            Person person = new Person("Barney", "Rubble", "brubble@gmail.com");
            const string bio = "test bio";
            const string avatarurl = "me.jpg";
            const string speakerKey = "key";
            Speaker speaker = new Speaker(person, speakerKey, bio, avatarurl);
            Speaker speaker2 = new Speaker(person, speakerKey + "extra", bio, avatarurl);

            Assert.That(speaker, Is.Not.EqualTo(speaker2));
        }

        [Test]
        public void GetHashCodeShouldThrowExceptionOnNullProperties()
        {
            Person person = new Person();
            Speaker speaker = new Speaker(person, null, null, null);
            speaker.GetHashCode();
        }
    }
}