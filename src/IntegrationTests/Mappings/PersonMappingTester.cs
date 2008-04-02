using CodeCampServer.IntegrationTests.DataAccess;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.IntegrationTests.Mappings
{
    [TestFixture]
    public class PersonMappingTester : DatabaseTesterBase
    {
        [Test]
        public void ShouldSaveIsAdministrator()
        {
            var person = new Person("joe", "blow", "jb@gmail.com") {IsAdministrator = true};

            using(var session = getSession())
            {
                session.SaveOrUpdate(person);
                session.Flush();
            }

            using(var session2 = getSession())
            {
                var personFromDb = session2.Load<Person>(person.Id);
                Assert.That(personFromDb.IsAdministrator);
            }
        }

        [Test]
        public void ShouldSavePasswordHashAndSalt()
        {
            var person = new Person("hank", "williams", "hankw@aol.com");

            var util = new CryptoUtil();
            person.PasswordSalt = util.CreateSalt();
            person.Password = util.HashPassword("apples", person.PasswordSalt);

            using(var session = getSession())
            {
                session.SaveOrUpdate(person);
                session.Flush();
            }

            using(var session2 = getSession())
            {
                var personFromDb = session2.Load<Person>(person.Id);
                Assert.That(personFromDb.PasswordSalt, Is.EqualTo(person.PasswordSalt));
                Assert.That(personFromDb.Password, Is.EqualTo(person.Password));
            }
        }
        
    }
}
